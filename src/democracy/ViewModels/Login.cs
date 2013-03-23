using System;
using System.Collections.Generic;
using System.Linq;

namespace democracy.ViewModels
{
    public class Login : ViewModel
    {
        public bool Errored { get; set; }
        public bool HasUsername { get; set; }
        public string Username { get; set; }

        public Login()
            : base(loggedIn: false)
        {
        }

        public bool OkButHasUsername
        {
            get
            {
                return !Errored && HasUsername;
            }
        }
    }
}
