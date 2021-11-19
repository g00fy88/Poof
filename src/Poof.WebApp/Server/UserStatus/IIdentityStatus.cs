using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.WebApp.Server.UserStatus
{
    public interface IIdentityStatus
    {
        IList<string> ChangedStati(string userId);

        void Add(string userId, string name);

        void Remove(string userId, string name);
    }
}
