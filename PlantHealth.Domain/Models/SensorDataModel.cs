using System;
using System.Diagnostics.CodeAnalysis;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlantHealth.Domain.Models;

[SuppressMessage("Usage", "CS8618", Justification = "Model Class")]
public class SensorDataModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id
    {
        get;
        set;
    }

    public string SerialNumber
    {
        get;
        set;
    }

    public double? SoilHumidity
    {
        get;
        set;
    }

    public double? Temperature
    {
        get;
        set;
    }

    public double? AirHumidity
    {
        get;
        set;
    }

    public DateTime Timestamp
    {
        get;
        set;
    }
}