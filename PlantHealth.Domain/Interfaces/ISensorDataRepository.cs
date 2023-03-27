using System.Collections.Generic;
using System.Threading.Tasks;

using PlantHealth.Domain.Models;

namespace PlantHealth.Domain.Interfaces;

public interface ISensorDataRepository
{
    public Task CreateAsync(SensorDataModel sensorData);
    public Task<SensorDataModel?> GetAsync(string id);

    public Task<IReadOnlyCollection<SensorDataModel>> GetBySerialNumberAsync(string serialNumber);

    public Task<IReadOnlyCollection<SensorDataModel>> GetAsync();
}