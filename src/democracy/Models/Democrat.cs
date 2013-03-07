using System;
using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace democracy.Models
{
    public class Democrat : IUserIdentity
    {
        public string UserName { get; set; }

        public IEnumerable<string> Claims
        {
            get;
            set;
        }
    }
}