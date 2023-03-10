using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using AutoBogus;

using Confluent.Kafka;
using Confluent.Kafka.Admin;

using Kafka.Library;

namespace Kafka.Producer;

public class Program
{
    private const string Topic = "PlantHealth";
    private const int MaxSleep = 5000;

    private readonly ProducerConfig _producerConfig;
    private readonly AdminClientConfig _adminClientConfig;
    private readonly Random _random;

    public Program()
    {
        _producerConfig = new ProducerConfig
                          {
                              BootstrapServers = "localhost:9092",
                              Acks = Acks.All,
                              QueueBufferingMaxMessages = 100_000,
                              AllowAutoCreateTopics = false
                          };
        
         _adminClientConfig = new AdminClientConfig
                              {
                                  BootstrapServers = _producerConfig.BootstrapServers
                              };


        _random = new Random(Guid.NewGuid().GetHashCode());
    }

    public static Task Main(string[] args)
    {
        return new Program().MainAsync();
    }

    private async Task MainAsync()
    {
        using IProducer<Null, string>? producer = new ProducerBuilder<Null, string>(_producerConfig).Build();

        await CreateTopic();

        while (!Console.KeyAvailable || Console.ReadKey().Key != ConsoleKey.Enter)
        {
            string jsonData = GenerateSensorDataString();
            await ProduceMessage(producer, jsonData);
            await Task.Delay(_random.Next(MaxSleep));
        }
    }

    private static string GenerateSensorDataString()
    {
        SensorData sensorData = new AutoFaker<SensorData>()
                                .RuleFor(data => data.SerialNumber, faker => faker.Database.Random.Guid().ToString())
                                .Generate();

        return JsonSerializer.Serialize(sensorData);
    }

    private async Task ProduceMessage(IProducer<Null, string> producer, string sensorData)
    {
        await producer.ProduceAsync(Topic, new Message<Null, string>
                                           {
                                               Value = sensorData
                                           });
    }

    private async Task CreateTopic()
    {

        using (IAdminClient adminClient = new AdminClientBuilder(_adminClientConfig).Build())
        {
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification>
                                                    {
                                                        new TopicSpecification
                                                        {
                                                            Name = Topic,
                                                            NumPartitions = 3,
                                                            ReplicationFactor = 3
                                                        }
                                                    });
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        }
    }
}