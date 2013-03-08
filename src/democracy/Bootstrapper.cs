using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace democracy
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            FormsAuthentication.Enable(pipelines, new FormsAuthenticationConfiguration
            {
                RedirectUrl = "~/login",
                UserMapper = container.Resolve<IUserMapper>(),
            });

            /** CREATE DEFAULT ADMIN USER IF NONE EXIST **/
            if (Democrats.All().Count() == 0)
            {
                Democrats.Save(Democrat.Create(
                    userName: "admin",
                    password: "strong password",
                    votes: 0,
                    claims: "admin"));
            }
        }
    }
}