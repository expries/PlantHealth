using System;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;
using Confluent.Kafka.Admin;

using Kafka.Producer.Interfaces;

namespace Kafka.Producer;

public class Program
{
    private readonly ISensorDataProducer _producer;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

    private Program()
    {
        _producer = new SensorDataProducer(
            new TopicSpecification
            {
                Name = "PlantHealth",
                NumPartitions = 3,
                ReplicationFactor = 3
            },
            new ProducerConfig
            {
                BootstrapServers = "localhost:9092",
                Acks = Acks.All,
                QueueBufferingMaxMessages = 100_000,
                AllowAutoCreateTopics = false
            });

    }

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        //await _producer.CreateTopicAsync();
        var produce = _producer.ProduceAsync(_cancellationTokenSource.Token);

        while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Enter)
        {
            await Task.Delay(1000, _cancellationTokenSource.Token);
        }

        _cancellationTokenSource.Cancel();
        await produce;
    }
}