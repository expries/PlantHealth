using System.Text;

using Confluent.Kafka;

using Kafka.Library;

using Newtonsoft.Json;

namespace Kafka.Producer;

public class SensorDataSerializer : ISerializer<SensorData>
{
    public byte[] Serialize(SensorData data, SerializationContext context)
    {
        var json = JsonConvert.SerializeObject(data);
        return Encoding.UTF8.GetBytes(json);
    }
}