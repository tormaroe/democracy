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
    public class Democrats : Repository<Democrat>, IUserMapper
    {
        public Democrats() : base("democrats") { }

        public Guid? Validate(string username, string password)
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
            return Collection.FindOneById(identifier);
        }

        public Democrat LoadByUsername(string username)
        {
            return Collection.FindOne(Query.EQ("UserName", username));
        }

        public void ChangePassword(Guid id, string password)
        {
            var user = Collection.FindOneById(id);
            user.SetPassword(password);
            Save(user);
        }

        public bool IsUsernameTaken(string username)
        {
            return Collection.Count(Query.EQ("UserName", username)) > 0;
        }

    }
}