using System;
using System.Text;
using System.Text.Json;

using Confluent.Kafka;

using PlantHealth.Domain.Kafka;

namespace PlantHealth.Kafka.Serializer;

public class SensorDataDeserializer : IDeserializer<SensorData>
{
    public SensorData Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
    {
        string json = Encoding.UTF8.GetString(data);
        return JsonSerializer.Deserialize<SensorData>(json)!;
    }
}