using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CounterBenchmark.Entities
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Counter> Counters { get; set; }
    }

    public class Counter
    {
        protected Counter()
        {
            
        }
        public Counter(string key)
        {
            Key = key;
            Value = 1;
        }
        [StringLength(128)]
        [Key]
        public string Key { get; set; }

        public long Value { get; set; }
    }
}