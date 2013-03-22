using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;

namespace democracy
{
    public class AdminController : NancyModule
    {
        public AdminController()
        {
            this.RequiresClaims(new []{"admin"});

            Get["/users"] = _ => View["admin.users.html", new ViewModels.AdminUsers { }];

            Get["/admin"] = _ => View["admin.items.html", new ViewModels.AdminItems { }];

            Post["/admin"] = parameters =>
            {
                var posted = this.Bind<VotingItem>();
                new VotingItems().Save(posted);
                Context.CurrentUser.AuditNewItem(posted);
                return Response.AsRedirect("~/admin");
            };

            Get["/admin/delete/{id}"] = parameters =>
            {
                var id = new Guid(parameters.id);
                new VotingItems().Delete(id);
                Context.CurrentUser.AuditItemRemoved(id);
                return Response.AsRedirect("~/admin");
            };


            Get["/reset"] = _ =>
            {
                return @"
                        <form action=reset-data method=POST>
                            <button type=submit>RESET DATA</button>
                        </form>";
            };

            Post["/reset-data"] = _ =>
            {
                new VotingItems().Drop();
                new Audit().Drop();
                return Response.AsRedirect("~/");
            };
        }
    }
}