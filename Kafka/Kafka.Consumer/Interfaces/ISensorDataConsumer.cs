using System.Threading;
using System.Threading.Tasks;

namespace Kafka.Consumer.Interfaces;

public interface ISensorDataConsumer
{
    public Task ConsumeAsync(CancellationToken ct);
}