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
    public class AuditController : NancyModule
    {
        public AuditController()
        {
            this.RequiresClaims(new[] { "admin" });

            Get["/audit"] = _ => View["admin.audit.html", new ViewModels.Audit { }];

        }
    }
}
