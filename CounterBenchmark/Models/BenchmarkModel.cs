using System;
using System.Diagnostics;

namespace CounterBenchmark.Models
{
    public class BenchmarkModel
    {

        public BenchmarkModel(Stopwatch stopwatch, long durations, string name, long lastValue)
        {
            Time = DateTime.Now;
            Durations = durations;
            Name = name;
            LastValue = lastValue;
            ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            DurationsPerSeconds = Calculate(Durations, ElapsedMilliseconds);
        }

        public DateTime Time { get; set; }

        public double DurationsPerSeconds { get; set; }

        private double Calculate(double durations, double elapsedMilliseconds)
        {
            return durations/(elapsedMilliseconds/1000);
        }

        public long ElapsedMilliseconds { get; set; }

        public long Durations { get; set; }
        public string Name { get; set; }
        public long LastValue { get; set; }
    }
}