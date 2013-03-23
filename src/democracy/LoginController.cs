using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;
using democracy.ViewModels;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Extensions;

namespace democracy
{
    public class LoginController : NancyModule
    {
        public LoginController()
        {
            Get["/login"] = parameters => View["login.html", new Login
            {
                Errored = this.Request.Query.error.HasValue,
                HasUsername = this.Request.Query.username.HasValue,
                Username = this.Request.Query.username,
            }];

            Get["/logout"] = parameters => this.LogoutAndRedirect("~/login");

            Post["/login"] = parameters => {
                var userGuid = new DB.Democrats().Validate((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return this.Context.GetRedirect("~/login?error=true&username=" + (string)this.Request.Form.Username);
                } 
                
                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/activate-user"] = parameters => View["activate-user.html"];

            Post["/activate-user"] = parameters =>
            {
                string username = Request.Form.Username;
                string password = Request.Form.Password;
                string token = Request.Form.Token;

                new RegistrationTokens().UseToken(token); // Throws if token does not exist - TODO: present error to user

                // TODO: Validate username uniqueness - not unique, present error

                // TODO: client side validate password

                new Democrats().Save(Democrat.Create(
                    userName: username,
                    password: password,
                    votes: Settings.NumberOfVotesPrUser));

                return Response.AsRedirect("~/");
            };
        }
    }
}