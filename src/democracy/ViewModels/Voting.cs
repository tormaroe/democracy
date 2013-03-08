using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using democracy.Models;

namespace democracy.ViewModels
{
    public class Voting
    {
        public IEnumerable<VotingItem> items { get; set; }

        public Voting()
        {
            items = new List<VotingItem>()
            {
                new VotingItem
                {
                    Category = "Product 1",
                    ShortDescription = "Fix some bug with feature A",
                    LongDescription = "LKjl kjlk jakl jalkj lkzdj ladksjf lasdkjf ladj f",
                    Link = "http://google.com",
                    Created = DateTime.UtcNow,
                },
                new VotingItem
                {
                    Category = "Product 1",
                    ShortDescription = "Fix some bug with feature B",
                    LongDescription = "LKjl kjlk jakl jalkj lkzdj ladksjf lasdkjf ladj f",
                    Link = "http://google.com",
                    Created = DateTime.UtcNow,
                },
            };
        }
    }
}