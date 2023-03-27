using AutoMapper;

using PlantHealth.Domain.Kafka;
using PlantHealth.Domain.Models;

namespace PlantHealth.Domain.Mapper;

public class SensorDataProfile : Profile
{
    public SensorDataProfile()
    {
        CreateMap<SensorData, SensorDataModel>();
    }
}