using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class AdminItems : ViewModel
    {
        public IEnumerable<VotingItem> items { get; set; }

        public AdminItems() : base(isAdmin: true, activeView: "items")
        {
            items = new VotingItems().All()
                .OrderByDescending(item => item.AbsoluteVotes);
        }
    }
}