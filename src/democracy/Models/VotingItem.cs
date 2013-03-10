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
        private string _Link;
        public string Link
        {
            get
            {
                return _Link;
            }
            set
            {
                _Link = value;
                if (_Link != null && !_Link.StartsWith("http", StringComparison.InvariantCultureIgnoreCase))
                    _Link = String.Format("http://{0}", _Link);
            }
        }
        public string Category { get; set; }
        public DateTime Created { get; set; }

        public VotingItem()
        {
            Id = Guid.NewGuid();
            Created = DateTime.UtcNow;
        }

        [BsonIgnore]
        public int AbsoluteVotes
        {
            get
            {
                return UpVotes - DownVotes;
            }
        }
    }
}