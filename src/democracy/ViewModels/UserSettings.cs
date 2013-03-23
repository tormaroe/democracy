using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class UserSettings : ViewModel
    {
        public UserSettings()
            : base(activeView: "usersettings")
        {
        }
    }
}
