using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PlantHealth.Api.Contracts.Dto;
using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Models;

namespace PlantHealth.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly ILogger<SensorDataController> _logger;
    private readonly ISensorDataService _sensorDataService;

    public SensorDataController(
        ILogger<SensorDataController> logger,
        ISensorDataService sensorDataService)
    {
        _logger = logger;
        _sensorDataService = sensorDataService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<SensorDataDto>>> Get()
    {
        IReadOnlyCollection<SensorDataModel> sensorData = await _sensorDataService.GetSensorDataAsync();

        return Ok(sensorData.Select(ConvertToDto));
    }

    [HttpGet("{serialNumber}")]
    public async Task<ActionResult<IReadOnlyCollection<SensorDataDto>>> Get(string serialNumber)
    {
        IReadOnlyCollection<SensorDataModel> sensorData = await _sensorDataService.GetSensorDataBySerialNumberAsync(serialNumber);

        if (sensorData is null)
            return NotFound();

        return Ok(sensorData.Select(ConvertToDto));
    }

    private SensorDataDto ConvertToDto(SensorDataModel sensorData)
    {
        return new SensorDataDto(
            sensorData.Id,
            sensorData.SerialNumber,
            sensorData.SoilHumidity,
            sensorData.Temperature,
            sensorData.AirHumidity,
            sensorData.Timestamp
        );
    }
}