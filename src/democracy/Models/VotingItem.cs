using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace democracy.Models
{
    public class VotingItem
    {
        [BsonId]
        public Guid Id { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Link { get; set; }
        public string Category { get; set; }
        public DateTime Created { get; set; }

        public VotingItem()
        {
            Id = Guid.NewGuid();
        }
    }
}