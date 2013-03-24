using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Voting : ViewModel
    {
        public IEnumerable<VotingItem> items { get; private set; }

        public int votesLeft { get; private set; }

        public Voting(string username)
            : base(activeView: "vote")
        {
            items = new VotingItems().All()
                .OrderByDescending(item => item.AbsoluteVotes);

            votesLeft = new Democrats().LoadByUsername(username).RemainingVotes;

            MessageAlertClass = "alert-success";
        }

        public string MessageAlertClass { get; set; }
        public bool HasMessage { get { return !string.IsNullOrWhiteSpace(Message); } }
        public string Message { get; set; }
    }
}