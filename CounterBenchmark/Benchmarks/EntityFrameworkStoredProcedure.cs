using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CounterBenchmark.Entities;

namespace CounterBenchmark.Benchmarks
{
    public class EntityFrameworkStoredProcedure : IBenchmark
    {
        public  async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;
            using (var db = new ApplicationDbContext())
            {
                for (var i = 0; i < durations; i++)
                {
                   var elements = db.Database.SqlQuery<long>("exec IncrementCounter {0}", key);
                    lastValue = await elements.SingleAsync();
                }
            }
            return lastValue;
        }
    }
}