using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using democracy.DB;

namespace democracy.ViewModels
{
    public class AdminUsers
    {
        public IEnumerable<User> users {get; private set; }

        public AdminUsers()
        {
            users = new Democrats()
                .All()
                .Select(d => new User
                {
                    Name = d.UserName,
                    LastLogin = d.LastLogin,
                });
        }

        public class User
        {
            public string Name { get; set; }
            public DateTime LastLogin { get; set; }
        }
    }
}