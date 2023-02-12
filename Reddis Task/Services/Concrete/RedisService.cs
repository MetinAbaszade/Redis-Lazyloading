using Microsoft.Extensions.Caching.Distributed;
using Reddis_Task.Services.Abstract;
using StackExchange.Redis;

namespace Reddis_Task.Services.Concrete
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase Database;

        public RedisService(IDatabase redisDatabase)
        {
            Database = redisDatabase;
        }

        public async void AddDataAsync(string key, string value, TimeSpan ttl)
        {
            if (key != null)
            {
                await Database.StringSetAsync(key, value, ttl);
            }
            else
            {
                throw new ArgumentException("Key cannot be null.");
            }
        }

        public async Task<string> GetDataAsync(string key)
        {
            if (key != null)
            {
                return await Database.StringGetAsync(key);
            }
            else
            {
                throw new ArgumentException("Key cannot be null.");
            }
        }

        public async void RemoveDataAsync(string key)
        {
            if (key != null)
            {
                await Database.KeyDeleteAsync(key);
            }
            else
            {
                throw new ArgumentException("Key cannot be null.");
            }
        }

        public async void EditDataAsync(string key, string value)
        {
            if (key != null)
            {
                await Database.StringSetAsync(key, value);
            }
            else
            {
                throw new ArgumentException("Key cannot be null.");
            }
        }
    }
}
