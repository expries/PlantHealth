namespace PlantHealth.Api.Contracts.Dto;

public record SensorDataDto
(
    string Id,
    string SerialNumber,
    double? SoilHumidity,
    double? Temperature,
    double? AirHumidity,
    DateTime Timestamp
);