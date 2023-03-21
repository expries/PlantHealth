using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Confluent.Kafka;

using Kafka.Consumer.Interfaces;
using Kafka.Library;

using Newtonsoft.Json;

namespace Kafka.Consumer;

public class SensorDataConsumer : ISensorDataConsumer
{
    private readonly ConsumerConfig _consumerConfig;
    private readonly string _topic;

    public SensorDataConsumer(string topic, ConsumerConfig consumerConfig)
    {
        _consumerConfig = consumerConfig;
        _topic = topic;
    }

    public Task ConsumeAsync(CancellationToken ct) => Task.Run(() => Consume(ct), ct);

    private void Consume(CancellationToken ct)
    {
        using var consumer = new ConsumerBuilder<string, SensorData>(_consumerConfig)
            .SetValueDeserializer(new SensorDateDeserializer())
            .Build();
        
        consumer.Subscribe(_topic);

        while (!ct.IsCancellationRequested)
        {
            var sensorData = consumer.Consume(ct);
            PrintData(sensorData);
        }

        consumer.Close();
    }

    private static void PrintData(ConsumeResult<string, SensorData> sensorData)
    {
        Console.WriteLine($"Topic: {sensorData.Topic}{Environment.NewLine}" + 
                          $"{sensorData}{Environment.NewLine}");
    }
}

internal class SensorDateDeserializer : IDeserializer<SensorData>
{
    public SensorData Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        var json = Encoding.UTF8.GetString(data);
        return JsonConvert.DeserializeObject<SensorData>(json);
    }
}