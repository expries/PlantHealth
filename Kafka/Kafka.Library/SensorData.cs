using System;

namespace Kafka.Library;

public record SensorData
(
    string SerialNumber,
    double? SoilHumidity,
    double? Temperature,
    double? AirHumidity,
    DateTime Timestamp
);
