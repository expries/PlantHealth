using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Producer.Interfaces;

public interface ISensorDataProducer
{
    public Task CreateTopicAsync();

    public Task ProduceAsync(CancellationToken ct);
}