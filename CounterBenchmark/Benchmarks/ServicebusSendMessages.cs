using System.Configuration;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace CounterBenchmark.Benchmarks
{
    [DataContract]
    public class AnonymousClass
    {
        [DataMember]
        public long Counter { get; set; }

        public AnonymousClass()
        {
            
        }

        public AnonymousClass(long counter)
        {
            Counter = counter;
        }
    }

    public class ServicebusSendMessages : IBenchmark
    {
        public async  Task<long> Run(long durations)
        {
            var key = GetType().Name;
            var client =
                QueueClient.CreateFromConnectionString(
                    ConfigurationManager.ConnectionStrings["servicebus"].ConnectionString, "dncbenchmark");

            for (var i = 0; i < (durations ); i++)
            {
                var message = new BrokeredMessage(new AnonymousClass(i));
                await client.SendAsync(message);
            }
            return durations;
        }
    }

    public class ServicebusSendMessages10 : IBenchmark
    {
        public Task<long> Run(long durations)
        {
            var key = GetType().Name;
            var client =
                QueueClient.CreateFromConnectionString(
                    ConfigurationManager.ConnectionStrings["servicebus"].ConnectionString, "dncbenchmark");

            for (var i = 0; i < (durations / 10); i++)
            {
                BrokeredMessage[] messages =
                {
                    new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i)),
                        new BrokeredMessage(new AnonymousClass(i))
                };
                Task.WaitAll(
                    client.SendAsync(messages[0]),
                    client.SendAsync(messages[1]),
                    client.SendAsync(messages[2]),
                    client.SendAsync(messages[3]),
                    client.SendAsync(messages[4]),
                    client.SendAsync(messages[5]),
                    client.SendAsync(messages[6]),
                    client.SendAsync(messages[7]),
                    client.SendAsync(messages[8]),
                    client.SendAsync(messages[9])
                    );
            }
            return Task.FromResult(durations);
        }
    }
}