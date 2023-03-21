using System;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

using Confluent.Kafka;

using Kafka.Library;

using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Table;

namespace Kafka.Producer;

public class TemperatureAggregate
{
    public double Total = 0;

    public int Count = 0;

    public double Average = 0;
}

public class SensorTemperatureAggregator
{
    private readonly string _topicName;
    private readonly string _destinationTopicName;
    private readonly ProducerConfig _producerConfig;
    private const string ApplicationId = "AverageTemperature";
    
    public SensorTemperatureAggregator(string topicName, string destinationTopicName, ProducerConfig producerConfig)
    {
        _topicName = topicName;
        _destinationTopicName = destinationTopicName;
        _producerConfig = producerConfig;
    }
    
    public Task AggregateAsync()
    {
        var builder = new StreamBuilder();

        builder
            .Stream<string, SensorData>(_topicName, new StringSerDes(), new JsonSerDes<SensorData>())
            .GroupByKey()
            .Aggregate(GetInitialAggregate, CalculateAverageTemperature, InMemory.As<string, TemperatureAggregate>())
            .ToStream()
            .To(_destinationTopicName);

        var topology = builder.Build();
        var stream = new KafkaStream(topology, new StreamConfig
                                               {
                                                   ApplicationId = ApplicationId, 
                                                   BootstrapServers = _producerConfig.BootstrapServers,
                                                   DefaultKeySerDes = new StringSerDes(),
                                                   DefaultValueSerDes = new JsonSerDes<TemperatureAggregate>()
                                               });
        return stream.StartAsync();
    }

    private static TemperatureAggregate CalculateAverageTemperature(string sensorSerial, SensorData sensorData, TemperatureAggregate aggregate)
    {
        if (sensorData.Temperature is null)
        {
            return aggregate;
        }

        aggregate.Count++;
        aggregate.Total += sensorData.Temperature.Value;
        aggregate.Average = Math.Round(aggregate.Total / aggregate.Count, 2);
        
        return aggregate;
    }

    private static TemperatureAggregate GetInitialAggregate() => new TemperatureAggregate
                                                                 {
                                                                     Average = 0,
                                                                     Count = 0,
                                                                     Total = 0
                                                                 };
}