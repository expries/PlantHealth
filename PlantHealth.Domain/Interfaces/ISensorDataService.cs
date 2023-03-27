using System.Collections.Generic;
using System.Threading.Tasks;

using PlantHealth.Domain.Models;

namespace PlantHealth.Domain.Interfaces;

public interface ISensorDataService
{
    public Task<SensorDataModel?> GetSensorDataAsync(string id);
    public Task<IReadOnlyCollection<SensorDataModel>> GetSensorDataBySerialNumberAsync(string serialNumber);
    public Task<IReadOnlyCollection<SensorDataModel>> GetSensorDataAsync();
    public Task CreateSensorDataAsync(SensorDataModel sensorData);
}