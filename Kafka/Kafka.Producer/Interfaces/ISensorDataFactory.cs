using Kafka.Library;

namespace Kafka.Producer.Interfaces;

public interface ISensorDataFactory
{
    public SensorData Generate();
}