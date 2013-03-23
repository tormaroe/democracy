using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;
using MongoDB.Driver.Builders;

namespace democracy.DB
{
    public class RegistrationTokens : Repository<RegistrationToken>
    {
        public RegistrationTokens() : base("registrationtokens") { }

        public void Generate()
        {
            Save(new RegistrationToken { Value = RandomTokenValue() });
        }

        private string RandomTokenValue()
        {
            const int tokenLength = 5;
            const string alphabet = "ABCDEFGHJKMNPQRSTUVWXYZÆ23456789-@";
            var rnd = new Random();

            return (new int[tokenLength])
                .Select(_ => rnd.Next(alphabet.Length))
                .Select(i => alphabet[i])
                .Aggregate("", (a, b) => a + b);
        }

        public void UseToken(string tokenValue)
        {
            var token = Collection.FindOne(Query.EQ("Value", tokenValue));

            if (token == null)
                throw new ArgumentException(String.Format("Token '{0}' not found!", tokenValue));

            Collection.Remove(Query.EQ("_id", token.Id));
        }
    }
}
