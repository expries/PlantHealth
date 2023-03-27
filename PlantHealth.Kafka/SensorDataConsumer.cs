using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Kafka;
using PlantHealth.Kafka.Serializer;

namespace PlantHealth.Kafka;

public class SensorDataConsumer : ISensorDataConsumer
{
    private readonly IConsumer<string, SensorData> _consumer;

    public SensorDataConsumer(string topic, ConsumerConfig consumerConfig)
    {
        _consumer = new ConsumerBuilder<string, SensorData>(consumerConfig)
                    .SetValueDeserializer(new SensorDataDeserializer())
                    .Build();

        _consumer.Subscribe(topic);
    }

    public Task<SensorData> GetSensorDataAsync(CancellationToken cancellationToken)
    {
        ConsumeResult<string, SensorData> sensorData = _consumer.Consume(cancellationToken);

        return Task.FromResult(sensorData.Message.Value);
    }
}