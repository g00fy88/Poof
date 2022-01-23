using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poof.DB.Data
{
    public sealed class DbCache<T> : ICache<T> where T: class
    {
        private readonly DbSet<T> dataset;
        private readonly IDictionary<string, T> cacheList;

        public DbCache(DbSet<T> dataset)
        {
            this.dataset = dataset;
            this.cacheList = new Dictionary<string, T>();
        }

        public void Clear()
        {
            lock(this.cacheList)
            {
                this.cacheList.Clear();
            }
        }

        public T Value(string id)
        {
            lock (this.cacheList)
            {
                T result;
                if (this.cacheList.ContainsKey(id))
                {
                    result = this.cacheList[id];
                }
                else
                {
                    result = this.dataset.Find(id);
                    this.cacheList[id] = result;
                }
                return result;
            }
        }
    }
}
