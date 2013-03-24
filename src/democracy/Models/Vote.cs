using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace democracy.Models
{
    public class Vote
    {
        public Guid ItemId { get; set; }
        public string ShortDescription { get; set; }
        public bool IsUpVote { get; set; }
        public DateTime Created { get; set; }

        public Vote()
        {
            Created = DateTime.UtcNow;
        }
    }

    public class VoteResult
    {
        public string Message { get; set; }
        public bool VoteOk { get; set; }
    }
}
