using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using democracy.Models;
using MongoDB.Driver.Builders;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Security;

namespace democracy.DB
{
    public class Democrats : IUserMapper
    {
        private const string COLLECTION_NAME = "democrats";

        public static Guid? Validate(string username, string password)
        {
            var user = LoadByUsername(username);

            if (user != null && user.ValidatePassword(password))
            {
                user.LastLogin = DateTime.UtcNow;
                Save(user);
                return user.Id;
            }

            return null;
        }

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            return Database.Instance.GetCollection<Democrat>(COLLECTION_NAME)
                .FindOneById(identifier);
        }

        public static Democrat LoadByUsername(string username)
        {
            return Database.Instance.GetCollection<Democrat>(COLLECTION_NAME)
                .FindOne(Query.EQ("UserName", username));
        }

        public static IEnumerable<Democrat> All()
        {
            return Database.Instance.GetCollection<Democrat>(COLLECTION_NAME)
                .FindAll();
        }

        public static void Save(Democrat d)
        {
            Database.Instance.GetCollection(COLLECTION_NAME).Save(d);
        }
    }
}