using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.Extensions.Hosting;

using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Kafka;
using PlantHealth.Domain.Models;

namespace PlantHealth.Worker;

public class DataWorker : BackgroundService
{
    private readonly IMapper _mapper;
    private readonly ISensorDataConsumer _sensorDataConsumer;
    private readonly ISensorDataRepository _sensorDataRepository;

    public DataWorker(ISensorDataConsumer sensorDataConsumer, ISensorDataRepository sensorDataRepository, IMapper mapper)
    {
        _sensorDataConsumer = sensorDataConsumer;
        _sensorDataRepository = sensorDataRepository;
        _mapper = mapper;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            SensorData sensorData = await _sensorDataConsumer.GetSensorDataAsync(cancellationToken);
            SensorDataModel modelObject = _mapper.Map<SensorDataModel>(sensorData);
            await _sensorDataRepository.CreateAsync(modelObject);
        }
    }
}