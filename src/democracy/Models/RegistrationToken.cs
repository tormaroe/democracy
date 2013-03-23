using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace democracy.Models
{
    public class RegistrationToken
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Value { get; set; }
    }
}
