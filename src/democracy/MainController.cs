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

            Get["/"] = _ => View["voting.html", new ViewModels.Voting { }];
        }
    }
}