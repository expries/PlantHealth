using System.Threading;
using System.Threading.Tasks;

using PlantHealth.Domain.Kafka;

namespace PlantHealth.Domain.Interfaces;

public interface ISensorDataConsumer
{
    public Task<SensorData> GetSensorDataAsync(CancellationToken cancellationToken);
}