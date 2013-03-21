using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Audit : ViewModel
    {
        public Audit()
            : base(activeView: "audit", isAdmin: true)
        {
        }
    }
}
