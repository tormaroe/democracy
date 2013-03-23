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
    public class MainController : NancyModule
    {
        public MainController()
        {
            this.RequiresAuthentication();

            Get["/"] = _ => View["voting.html", new ViewModels.Voting 
            { 
                IsAdmin = Context.CurrentUser.Claims.Contains("admin") 
            }];

            Get["/vote-up/{id}"] = parameters =>
            {
                DoVote(parameters.id, upVote: true);
                return Response.AsRedirect("/");
            };

            Get["/vote-down/{id}"] = parameters =>
            {
                DoVote(parameters.id, upVote: false);
                return Response.AsRedirect("/");
            };
        }

        private void DoVote(string id, bool upVote)
        {
            var itemKey = new Guid(id);

            // Verify that user has votes left

            // Check if it is a reversal of existing vote
            //      remove vote record
            //      reclaim vote on user
            // else
            //      Add vote record
            //      subtract vote on user

            // Audit!

            // Re-calculate aggregated vote value on item
        }
    }
}