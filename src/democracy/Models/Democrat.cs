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
        public int RemainingVotes { get; set; }
        public List<Vote> Votes { get; set; }

        private Democrat()
        {
            Claims = new List<string>();
            Votes = new List<Vote>();
        }

        public static Democrat Create(string userName, string password, int votes, params string[] claims)
        {
            var user = new Democrat
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                RemainingVotes = votes,
            };

            user.SetPassword(password);
            ((List<string>)user.Claims).AddRange(claims);

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
        public VoteResult Vote(VotingItem item, bool isUpVote)
        {
            var existingVote = Votes.FirstOrDefault(v => v.ItemId == item.Id);

            if (existingVote != null)
            {
                // Vote already exist for item

                if (IsVoteRetraction(isUpVote, existingVote))
                {
                    Votes.Remove(existingVote);
                    RemainingVotes++;
                    if (existingVote.IsUpVote)
                        item.UpVotes--;
                    else
                        item.DownVotes--;

                    return new VoteResult
                    {
                        VoteOk = true,
                        Message = string.Format("Your {0}vote for \"{1}\" vas removed!",
                            (existingVote.IsUpVote ? "up" : "down"),
                            item.ShortDescription),
                    };
                }
                else
                {
                    // double vote not allowed
                    return new VoteResult
                    {
                        VoteOk = false,
                        Message = "Vote already exist - nothing done!",
                    };
                }

            }
            else
            {
                if (RemainingVotes > 0)
                {
                    Votes.Add(new Vote
                    {
                        IsUpVote = isUpVote,
                        ItemId = item.Id,
                        ShortDescription = item.ShortDescription
                    });
                    RemainingVotes--;
                    if (isUpVote)
                        item.UpVotes++;
                    else
                        item.DownVotes++;

                    return new VoteResult
                    {
                        VoteOk = true,
                        Message = string.Format("Your {0}vote for \"{1}\" has been registered!",
                            (isUpVote ? "up" : "down"),
                            item.ShortDescription),
                    };
                }
                else
                {
                    return new VoteResult
                    {
                        VoteOk = false,
                        Message = "No votes left - nothing done!",
                    };
                }
            }

            // TODO: May need a way to re-calculate all vote values on items from votes, in case it gets out of sync

            throw new NotImplementedException();
        }

        private static bool IsVoteRetraction(bool isUpVote, Vote existingVote)
        {
            return (isUpVote && !existingVote.IsUpVote) || (!isUpVote && existingVote.IsUpVote);
        }
    }
}