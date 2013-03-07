using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;

namespace democracy
{
    public class AdminController : NancyModule
    {
        public AdminController()
        {
            this.RequiresClaims(new []{"admin"});

            Get["/users"] = _ => View["admin.users.html", new ViewModels.AdminUsers { }];
        }
    }
}