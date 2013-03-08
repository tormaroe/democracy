using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace democracy.Models
{
    public class AuditItem
    {
        public static string[] EventTypes = new[] {
            "User created",
            "Up-vote",
            "Down-vote",
            "New item",
            "Item removed",
            "Item changed",

        };

        [BsonId]
        public Guid Id { get; set; }

        public string Username { get; set; }
        public DateTime When { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }

        public AuditItem(string type, string description, string user)
        {
            ValidateType(type);

            EventType = type;
            Description = description;
            Username = user;
            When = DateTime.UtcNow;
        }

        private void ValidateType(string type)
        {
            if (EventTypes.FirstOrDefault(et => et == type) == null)
                throw new ArgumentOutOfRangeException("type", 
                    String.Format("Audit event type {0} not allowed!", type));
        }
    }
}
