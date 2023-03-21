using System;
using System.Collections.Generic;

using AutoBogus;

using Kafka.Library;
using Kafka.Producer.Interfaces;

namespace Kafka.Producer;

public class SensorDataFactory : ISensorDataFactory
{
    private readonly List<string> _serialNumbers = new List<string>
                                          {
                                              "3160e5fa-03fb-4f71-8b61-9808730405e5",
                                              "f4e8db58-4adc-41c8-8bc8-a0509383e36f",
                                              "879864ca-b8ae-4243-8954-3c04ab312b54",
                                              "9b859000-f8a7-42e0-81e7-f0d66440e13a",
                                              "21d183cf-b821-40a1-98c5-66c6d81feb3a"
                                          };
    
    public SensorData Generate() => new AutoFaker<SensorData>()
                                    .RuleFor(x => x.Temperature, f => f.Random.Double(-10, 25))
                                    .RuleFor(x => x.AirHumidity, f => f.Random.Double(0, 30))
                                    .RuleFor(x => x.Timestamp, f => DateTime.UtcNow)
                                    .RuleFor(x => x.SoilHumidity, f => f.Random.Double(0, 60))
                                    .RuleFor(x => x.SerialNumber, f => f.PickRandom(_serialNumbers))
                                    .Generate();
}