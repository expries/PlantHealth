using System;

namespace PlantHealth.Domain.Kafka;

public record SensorData
(
    string SerialNumber,
    double? SoilHumidity,
    double? Temperature,
    double? AirHumidity,
    DateTime Timestamp
);