using System;
using System.Collections.Generic;
using System.Linq;
using democracy.DB;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Voting : ViewModel
    {
        public IEnumerable<VotingItemViewWrapper> items { get; private set; }
        public int votesLeft { get; private set; }

        public string MessageAlertClass { get; set; }
        public bool HasMessage { get { return !string.IsNullOrWhiteSpace(Message); } }
        public string Message { get; set; }

        public Voting(string username)
            : base(activeView: "vote")
        {
            var user = new Democrats().LoadByUsername(username);

            int index = 0;

            items = new VotingItems().All()
                .OrderByDescending(item => item.AbsoluteVotes)
                .Select(vi => new VotingItemViewWrapper
                {
                    SequenceNumber = ++index,
                    item = vi,
                    VotedUp = user.Votes.FirstOrDefault(i => i.ItemId == vi.Id && i.IsUpVote) != null,
                    VotedDown = user.Votes.FirstOrDefault(i => i.ItemId == vi.Id && !i.IsUpVote) != null,
                })
                .ToList();

        
            votesLeft = user.RemainingVotes;
            MessageAlertClass = "alert-success";
        }

        public IEnumerable<int> UpVoteSequence
        {
            get
            {
                return items
                    .Where(i => i.VotedUp)
                    .Select(i => i.SequenceNumber);
            }
        }

        public IEnumerable<int> DownVoteSequence
        {
            get
            {
                return items
                    .Where(i => i.VotedDown)
                    .Select(i => i.SequenceNumber);
            }
        }

        public class VotingItemViewWrapper
        {
            public int SequenceNumber { get; set; }
            public VotingItem item { get; set; }
            public bool VotedUp { get; set; }
            public bool VotedDown { get; set; }
        }

    }
}