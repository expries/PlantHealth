using System;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

namespace Kafka.Consumer;

public class Program
{
    private readonly SensorDataConsumer _consumer;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    
    public Program()
    {
        _consumer = new SensorDataConsumer("PlantHealth", 
            new ConsumerConfig 
            { 
                BootstrapServers = "localhost:9092", 
                GroupId = "someId", 
                AutoOffsetReset = AutoOffsetReset.Earliest 
            });
    }

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        var consumerTask = _consumer.ConsumeAsync(_cancellationTokenSource.Token);

        while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Enter)
        {
            await Task.Delay(1000);
        }

        _cancellationTokenSource.Cancel();
        await consumerTask;
    }
    
}