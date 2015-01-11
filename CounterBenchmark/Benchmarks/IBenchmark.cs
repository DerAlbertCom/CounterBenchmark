using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CounterBenchmark.Benchmarks
{
    public interface IBenchmark
    {
        Task<long> Run(long durations);
    }
}