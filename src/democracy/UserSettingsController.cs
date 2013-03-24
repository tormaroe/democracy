using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;
using Nancy.Bootstrapper;
using Nancy.Authentication.Forms;

namespace democracy
{
    public class UserSettingsController : NancyModule
    {
        public UserSettingsController()
        {
            this.RequiresAuthentication();

            Get["/usersettings"] = _ => LoadView();

            Get["/change-password"] = _ => Response.AsRedirect("~/usersettings");

            Post["/change-password"] = _ =>
            {
                var repository = new DB.Democrats();
                var userGuid = repository.Validate(Context.CurrentUser.UserName, (string)this.Request.Form.OldPassword);

                if (!userGuid.HasValue)
                    return LoadView(s =>
                    {
                        s.Message = "Specified passord was not correct. Password was not changed!";
                        s.MessageAlertClass = "alert-error";
                    });

                repository.ChangePassword(userGuid.Value, (string)this.Request.Form.Password);

                return LoadView(s => s.Message = "Password changed!");
            };
        }

        private Nancy.Responses.Negotiation.Negotiator LoadView(Action<ViewModels.UserSettings> callback = null)
        {
            var viewModel = new ViewModels.UserSettings
            {
                IsAdmin = Context.CurrentUser.Claims.Contains("admin")
            };

            if (callback != null)
                callback.Invoke(viewModel);

            return View["usersettings.html", viewModel];
        }
    }
}
