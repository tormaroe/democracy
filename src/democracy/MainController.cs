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

            Get["/"] = _ => View["voting.html", new ViewModels.Voting(Context.CurrentUser.UserName)
            { 
                IsAdmin = Context.CurrentUser.Claims.Contains("admin"),
                Message = this.Request.Query.msg.HasValue ? this.Request.Query.msg : "", // WIP
            }];

            Get["/vote-up/{id}"] = parameters =>
            {
                DoVote(parameters.id, isUpVote: true);
                return Response.AsRedirect("/");
            };

            Get["/vote-down/{id}"] = parameters =>
            {
                DoVote(parameters.id, isUpVote: false);
                return Response.AsRedirect("/");
            };
        }

        private void DoVote(string id, bool isUpVote)
        {
            var itemKey = new Guid(id);
            var democrats = new DB.Democrats();
            var votingItems = new DB.VotingItems();

            var item = votingItems.FindById(itemKey);
            var user = democrats.LoadByUsername(Context.CurrentUser.UserName);

            var result = user.Vote(item, isUpVote);

            if (result.VoteOk)
            {
                democrats.Save(user);
                votingItems.Save(item);
            }

            // Audit result!
            // Respond to user based on result
        }
    }
}