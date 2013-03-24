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
            MessageAlertClass = "alert-success";
        }

        public string MessageAlertClass { get; set; }
        public bool HasMessage { get { return !string.IsNullOrWhiteSpace(Message); } }
        public string Message { get; set; }
    }
}
