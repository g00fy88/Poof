using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.WebApp.Server.UserStatus
{
    public sealed class SimpleStatus : IIdentityStatus
    {
        private readonly IDictionary<string, IList<string>> stati;

        public SimpleStatus()
        {
            this.stati = new Dictionary<string, IList<string>>();
        }

        public void Add(string userId, string name)
        {
            lock (this.stati)
            {
                if (!this.stati.ContainsKey(name))
                {
                    this.stati[userId] = new List<string>();
                }
                if(!this.stati[userId].Contains(name))
                {
                    this.stati[userId].Add(name);
                }
            }
        }

        public IList<string> ChangedStati(string userId)
        {
            lock (this.stati)
            {
                IList<string> result = new List<string>();
                if (this.stati.ContainsKey(userId))
                {
                    result = this.stati[userId];
                }
                return result;
            }
        }

        public void Remove(string userId, string name)
        {
            lock(this.stati)
            {
                if(this.stati.ContainsKey(userId))
                {
                    this.stati[userId].Remove(name);

                    if(this.stati[userId].Count == 0)
                    {
                        this.stati.Remove(userId);
                    }
                }
            }
        }
    }
}
