using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                return Response.AsRedirect("~/admin");
            };
        }
    }
}