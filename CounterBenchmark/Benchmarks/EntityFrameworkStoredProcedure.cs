using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using CounterBenchmark.Entities;

namespace CounterBenchmark.Benchmarks
{
    public class EntityFrameworkStoredProcedure : IBenchmark
    {
        public async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;
            using (var db = new ApplicationDbContext())
            {
                for (var i = 0; i < durations; i++)
                {
                    lastValue = await db.Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync();
                }
            }
            return lastValue;
        }
    }

    public class EntityFrameworkStoredProcedure10 : IBenchmark
    {
        public async Task<long> Run(long durations)
        {
            long lastValue = 0;
            var key = GetType().Name;
            DbContext[] dbs = {
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext(),
                new ApplicationDbContext()
            };
            for (var i = 0; i < durations/10; i++)
            {
                var values = await
                    Task.WhenAll(
                        dbs[0].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[1].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[2].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[3].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[4].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[5].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[6].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[7].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[8].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync(),
                        dbs[9].Database.SqlQuery<long>("exec IncrementCounter {0}", key).SingleAsync()
                        );
                lastValue = values.Max();
            }
            foreach (var db in dbs)
            {
                db.Dispose();
            }
            return lastValue;
        }
    }
}