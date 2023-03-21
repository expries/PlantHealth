using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;
using Confluent.Kafka.Admin;

using Kafka.Library;
using Kafka.Producer.Interfaces;

namespace Kafka.Producer;

public class SensorDataProducer : ISensorDataProducer
{
    private readonly ProducerConfig _producerConfig;
    private readonly AdminClientConfig _adminClientConfig;
    private readonly TopicSpecification _topicSpecification;
 
    private readonly ISensorDataFactory _sensorDataFactory;
    private readonly Random _random;
    private const int MaxSendingIntervalInSeconds = 5;

    public SensorDataProducer(TopicSpecification topicSpecification, ProducerConfig producerConfig)
    {
        _producerConfig = producerConfig;
        _topicSpecification = topicSpecification;
        _adminClientConfig = new AdminClientConfig { BootstrapServers = _producerConfig.BootstrapServers };
        _sensorDataFactory = new SensorDataFactory();
        _random = new Random(Guid.NewGuid().GetHashCode());
    }

    public async Task CreateTopicAsync()
    {
        using var adminClient = new AdminClientBuilder(_adminClientConfig).Build();

        try
        {
            await adminClient.CreateTopicsAsync(new List<TopicSpecification> { _topicSpecification });
        }
        catch (CreateTopicsException ex)
        {
            var result = ex.Results.FirstOrDefault();
            Console.WriteLine($"An error occured creating topic {result?.Topic}: {result?.Error.Reason}");
        }
    }
    
    public async Task ProduceAsync(CancellationToken ct)
    {
        using var producer = new ProducerBuilder<string, SensorData>(_producerConfig)
                             .SetValueSerializer(new SensorDataSerializer())
                             .Build();
        
        await RegisterAggregator();
        
        while (!ct.IsCancellationRequested)
        {
            var data = _sensorDataFactory.Generate();
            await SendToTopic(producer, data);
            await Task.Delay(_random.Next(MaxSendingIntervalInSeconds * 1000));
        }
    }

    private async Task RegisterAggregator()
    {
        var topicName = _topicSpecification.Name;
        var destinationTopicName = $"{topicName}_AvgTemperature";
        var aggregator = new SensorTemperatureAggregator(topicName, destinationTopicName, _producerConfig);
        await aggregator.AggregateAsync();
    }

    private async Task SendToTopic(IProducer<string, SensorData> producer, SensorData data)
    {
        var message = new Message<string, SensorData> { Key = data.SerialNumber, Value = data };
        await producer.ProduceAsync(_topicSpecification.Name, message);
    }
}