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

            Get["/usersettings"] = _ => View["usersettings.html", new ViewModels.UserSettings
            {
                IsAdmin = Context.CurrentUser.Claims.Contains("admin") 
            }];

            // TODO: Post-handler to change password
        }
    }
}
