using Microsoft.Extensions.Options;

using MongoDB.Driver;

using PlantHealth.Domain.Models;
using PlantHealth.Domain.Settings;

namespace PlantHealth.DataAccess;

public class SensorDataContext
{
    private readonly IMongoDatabase _database;
    private readonly string _sensorDataCollectionName;

    public SensorDataContext(IMongoClient client, IOptions<DatabaseSettings> settings)
    {
        _database = client.GetDatabase(settings.Value.DatabaseName);
        _sensorDataCollectionName = settings.Value.SensorDataCollectionName;
    }

    public IMongoCollection<SensorDataModel> SensorData => _database.GetCollection<SensorDataModel>(_sensorDataCollectionName);
}