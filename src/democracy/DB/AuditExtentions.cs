using System;
using System.Collections.Generic;
using System.Linq;
using democracy.Models;
using Nancy.Security;

namespace democracy.DB
{
    public static class AuditExtentions
    {
        public static void AuditUserCreated(this IUserIdentity user, string description)
        {
            Insert("User created", description, user);
        }

        public static void AuditUpVote(this IUserIdentity user, string description)
        {
            Insert("Up-vote", description, user);
        }

        public static void AuditDownVote(this IUserIdentity user, string description)
        {
            Insert("Down-vote", description, user);
        }

        public static void AuditNewItem(this IUserIdentity user, VotingItem item)
        {
            Insert("New item", 
                string.Format("{0}: \"{1}\"", item.Id, item.ShortDescription), 
                user);
        }

        public static void AuditItemRemoved(this IUserIdentity user, Guid id)
        {
            Insert("Item removed", id.ToString(), user);
        }

        public static void AuditItemChanged(this IUserIdentity user, string description)
        {
            Insert("Item changed", description, user);
        }

        private static void Insert(string type, string description, IUserIdentity user)
        {
            new Audit().Save(new AuditItem(type, description, user.UserName));
        }
    }
}
