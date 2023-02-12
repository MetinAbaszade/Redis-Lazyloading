using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Reddis_Task.Services.Abstract
{
    public interface IRedisService
    { 
        void AddDataAsync(string key, string value, TimeSpan ttl);
        void EditDataAsync(string key, string value);
        void RemoveDataAsync(string key);
        Task<string> GetDataAsync(string key);
    }
}
