using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;
using MongoDB.Driver.Builders;

namespace democracy.DB
{
    public class VotingItems : Repository<VotingItem>
    {
        public VotingItems() : base("votingitems") {}

    }
}