using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;

namespace democracy.ViewModels
{
    public class AdminUsers : ViewModel
    {
        public IEnumerable<User> users { get; private set; }
        public IEnumerable<string> tokens { get; private set; }

        public AdminUsers() : base(isAdmin: true, activeView: "users")
        {
            users = new Democrats()
                .All()
                .Select(d => new User
                {
                    Name = d.UserName,
                    LastLogin = d.LastLogin,
                    Claims = d.Claims,
                    RemainingVotes = d.RemainingVotes
                });

            tokens = new RegistrationTokens()
                .All()
                .Select(t => t.Value);
        }

        public class User
        {
            public string Name { get; set; }
            public DateTime LastLogin { get; set; }
            public IEnumerable<string> Claims { get; set; }
            public int RemainingVotes { get; set; }

            public string ClaimsString
            {
                get
                {
                    if (Claims.Count() == 0)
                        return "";
                    return Claims.Aggregate((a, b) => a + " " + b);
                }
            }
        }
    }
}