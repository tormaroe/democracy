﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Voting
    {
        public IEnumerable<VotingItem> items { get; set; }

        public Voting()
        {
            items = new VotingItems().All()
                .OrderByDescending(item => item.AbsoluteVotes);
        }
    }
}