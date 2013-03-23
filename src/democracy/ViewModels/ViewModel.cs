using System;
using System.Collections.Generic;
using System.Linq;

namespace democracy.ViewModels
{
    public class ViewModel
    {
        public bool LoggedIn { get; set; }
        public bool IsAdmin { get; set; }
        public string ActiveView { get; set; }

        public ViewModel(bool loggedIn = true, bool isAdmin = false, string activeView = "")
        {
            LoggedIn = loggedIn;
            IsAdmin = isAdmin;
            ActiveView = activeView;
        }
        
        private class MenuItem
        {
            public string Name { get; set; }
            public string Url { get; set; }
            public string Icon { get; set; }
            public string Text { get; set; }
            public bool AdminOnly { get; set; }

            public string ToHTML(string activeName)
            {
                return string.Format("<li{0}><a href=\"{1}\"><i class=\"{2}\"></i> {3}</a></li>",
                    (activeName == Name ? " class=\"active\"" : ""),
                    Url,
                    Icon,
                    Text);
            }
        }

        private List<MenuItem> _menu = new List<MenuItem>(){
            new MenuItem { Name = "vote", Url = "/", Icon = "icon-thumbs-up", Text = "Vote" },
            new MenuItem { Name = "users", Url = "users", Icon = "icon-user", Text = "Users", AdminOnly = true },
            new MenuItem { Name = "items", Url = "admin", Icon = "icon-cogs", Text = "Item Admin", AdminOnly = true },
            new MenuItem { Name = "audit", Url = "audit", Icon = "icon-film", Text = "Audit log", AdminOnly = true },
            new MenuItem { Name = "logout", Url = "logout", Icon = "icon-signout", Text = "Logout" },
        };

        public string Menu
        {
            get
            {
                return String.Concat(_menu
                    .Where(m => !m.AdminOnly || this.IsAdmin)
                    .Select(m => m.ToHTML(ActiveView)));
            }
        }
    }
}
