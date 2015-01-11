using System.Data.Entity;
using System.Threading.Tasks;
using CounterBenchmark.Entities;

namespace CounterBenchmark.Benchmarks
{
    public class EntityFrameworkClientSide : IBenchmark
    {
        public  async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;
            using (var db = new ApplicationDbContext())
            {
                for (var i = 0; i < durations; i++)
                {
                    var counter = await db.Counters.SingleOrDefaultAsync(c => c.Key == key);
                    if (counter == null)
                    {
                        counter = new Counter(key);
                        db.Counters.Add(counter);
                    }
                    else
                    {
                        counter.Value++;
                    }
                    await db.SaveChangesAsync();
                    lastValue = counter.Value;
                }
            }
            return lastValue;
        }
    }
}