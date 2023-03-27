using System.Collections.Generic;
using System.Threading.Tasks;

using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Models;

namespace PlantHealth.Domain.Services;

public class SensorDataService : ISensorDataService
{
    private readonly ISensorDataRepository _sensorDataRepository;

    public SensorDataService(ISensorDataRepository sensorDataRepository)
    {
        _sensorDataRepository = sensorDataRepository;
    }

    public async Task<SensorDataModel?> GetSensorDataAsync(string id)
    {
        return await _sensorDataRepository.GetAsync(id);
    }

    public async Task<IReadOnlyCollection<SensorDataModel>> GetSensorDataBySerialNumberAsync(string serialNumber)
    {
        return await _sensorDataRepository.GetBySerialNumberAsync(serialNumber);
    }

    public async Task<IReadOnlyCollection<SensorDataModel>> GetSensorDataAsync()
    {
        return await _sensorDataRepository.GetAsync();
    }

    public async Task CreateSensorDataAsync(SensorDataModel sensorData)
    {
        await _sensorDataRepository.CreateAsync(sensorData);
    }
}