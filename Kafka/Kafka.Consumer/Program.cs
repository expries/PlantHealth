using System;
using System.Text.Json;
using System.Threading.Tasks;

using Confluent.Kafka;

using Kafka.Library;

namespace Kafka.Consumer;

public class Program
{
    private const string Topic = "PlantHealth";
    private const int MaxSleep = 3000;
    private readonly Random _random;

    private readonly ConsumerConfig _consumerConfig;

    public Program()
    {
        _consumerConfig = new ConsumerConfig
                         {
                             BootstrapServers = "localhost:9092",
                             GroupId = "someId",
                             AutoOffsetReset = AutoOffsetReset.Earliest,
                         };

        _random = new Random(Guid.NewGuid().GetHashCode());
    }

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        using IConsumer<Null, string>? consumer = new ConsumerBuilder<Null, string>(_consumerConfig).Build();
        consumer.Subscribe(Topic);

        while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Enter)
        {
            ConsumeResult<Null, string>? consumeResult = consumer.Consume();
            PrintData(consumeResult);
            await Task.Delay(_random.Next(MaxSleep));
        }

        consumer.Close();
    }

    private void PrintData(ConsumeResult<Null, string> consumeResult)
    {
        SensorData? sensorData = JsonSerializer.Deserialize<SensorData>(consumeResult.Message.Value);

        if (sensorData is null)
        {
            Console.WriteLine($"Message is corrupted");
            return;
        }
        
        Console.WriteLine($"Topic: {consumeResult.Topic}" +
                          $"\n{sensorData}\n\n");
    }
}