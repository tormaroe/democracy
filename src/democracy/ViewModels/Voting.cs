﻿using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Voting : ViewModel
    {
        public IEnumerable<VotingItem> items { get; set; }

        public Voting()
            : base(activeView: "vote")
        {
            items = new VotingItems().All()
                .OrderByDescending(item => item.AbsoluteVotes);
        }
    }
}