using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace democracy.DB
{
    public class VotingItems : Repository<VotingItem>
    {
        public VotingItems() : base("votingitems") {}

        public VotingItem FindById(Guid id)
        {
            return Collection.FindOneById(id);
        }

        public void Delete(Guid id)
        {
            Collection.Remove(Query.EQ("_id", id));
        }

    }
}