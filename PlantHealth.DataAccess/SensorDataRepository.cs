using System.Collections.Generic;
using System.Threading.Tasks;

using MongoDB.Driver;

using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Models;

namespace PlantHealth.DataAccess;

public class SensorDataRepository : ISensorDataRepository
{
    private readonly SensorDataContext _sensorDataContext;

    public SensorDataRepository(SensorDataContext sensorDataContext)
    {
        _sensorDataContext = sensorDataContext;
    }

    public Task CreateAsync(SensorDataModel sensorData)
    {
        return _sensorDataContext.SensorData.InsertOneAsync(sensorData);
    }

    public async Task<SensorDataModel?> GetAsync(string id)
    {
        FilterDefinition<SensorDataModel> filter = Builders<SensorDataModel>.Filter.Eq("Id", id);

        return await _sensorDataContext.SensorData.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyCollection<SensorDataModel>> GetBySerialNumberAsync(string serialNumber)
    {
        FilterDefinition<SensorDataModel> filter = Builders<SensorDataModel>.Filter.Eq("SerialNumber", serialNumber);

        return await _sensorDataContext.SensorData.Find(filter).ToListAsync();
    }

    public async Task<IReadOnlyCollection<SensorDataModel>> GetAsync()
    {
        return await _sensorDataContext.SensorData.Find(_ => true).ToListAsync();
    }
}