using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using BookSleeve;
using StackExchange.Redis;

namespace CounterBenchmark.Benchmarks
{
    public class RedisIncrement : IBenchmark
    {
        public async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;

            using (var redis =  ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["redis"].ConnectionString))
            {
                var db = redis.GetDatabase();
                for (var i = 0; i < durations; i++)
                {
                    lastValue = await db.StringIncrementAsync(key);
                }
            }
            return lastValue;
        }
    }

    public class RedisIncrement10 : IBenchmark
    {
        public async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;

            using (var redis = ConnectionMultiplexer.Connect(ConfigurationManager.ConnectionStrings["redis"].ConnectionString))
            {
                var db = redis.GetDatabase();
                for (var i = 0; i < durations / 10; i++)
                {
                    var values = await Task.WhenAll(
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key), 
                        db.StringIncrementAsync(key) 
                        );
                    lastValue = values.Max();
                }
            }
            return lastValue;
        }
    }
}