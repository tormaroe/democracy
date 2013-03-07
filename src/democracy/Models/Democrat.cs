using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using democracy.DB;
using MongoDB.Bson.Serialization.Attributes;
using Nancy.Security;

namespace democracy.Models
{
    public class Democrat : IUserIdentity
    {
        
        [BsonId]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public DateTime LastLogin { get; set; }
        public string PasswordHash { get; private set; }
        public string PasswordSalt { get; set; }

        public Democrat()
        {
            Claims = new List<string>();
        }

        public static Democrat Create(string userName, string password)
        {
            var user = new Democrat
            {
                Id = Guid.NewGuid(),
                UserName = userName,
            };
            user.SetPassword(password);
            return user;
        }

        public void SetPassword(string password)
        {
            PasswordSalt = Guid.NewGuid().ToString("N").Substring(0, 8);
            PasswordHash = Password.Hash(password, PasswordSalt);
        }

        public bool ValidatePassword(string passwordCandidate)
        {
            return Password.Hash(passwordCandidate, PasswordSalt) == PasswordHash;
        }

        public static class Password
        {
            public static string Hash(string password, string salt)
            {
                using (var hashingAlgorithm = new MD5CryptoServiceProvider())
                {
                    byte[] data = Encoding.ASCII.GetBytes(password + salt);
                    data = hashingAlgorithm.ComputeHash(data);
                    return Encoding.ASCII.GetString(data);
                }
            }
        }
    }
}