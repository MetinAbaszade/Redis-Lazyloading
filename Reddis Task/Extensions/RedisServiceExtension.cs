    using Reddis_Task.Services.Abstract;
using Reddis_Task.Services.Concrete;
using StackExchange.Redis;

namespace Reddis_Task.Extensions
{
    public static class RedisServiceExtension
    {
        public static IServiceCollection AddRedisService(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDatabase>(x =>
            {
                var multiplexer = ConnectionMultiplexer.Connect(connectionString);
                return multiplexer.GetDatabase();
            });
            services.AddTransient<IRedisService, RedisService>();

            return services;
        }
    }
}
