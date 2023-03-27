using System.Reflection;

using Confluent.Kafka;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using MongoDB.Driver;

using PlantHealth.DataAccess;
using PlantHealth.Domain.Interfaces;
using PlantHealth.Domain.Mapper;
using PlantHealth.Domain.Services;
using PlantHealth.Domain.Settings;
using PlantHealth.Kafka;
using PlantHealth.Worker;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KafkaSettings>(
    builder.Configuration.GetSection("Kafka"));

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddTransient<ISensorDataService, SensorDataService>();
builder.Services.AddTransient<ISensorDataRepository, SensorDataRepository>();
builder.Services.AddTransient<ISensorDataConsumer>(provider =>
                                                   {
                                                       IOptions<KafkaSettings> settings = provider.GetRequiredService<IOptions<KafkaSettings>>();

                                                       return new SensorDataConsumer(settings.Value.Topic,
                                                           new ConsumerConfig
                                                           {
                                                               BootstrapServers = settings.Value.BootstrapServers,
                                                               GroupId = settings.Value.GroupId,
                                                               AutoOffsetReset = AutoOffsetReset.Earliest
                                                           });
                                                   });

builder.Services.AddSingleton<IMongoClient>(provider =>
                                            {
                                                IOptions<DatabaseSettings> settings = provider.GetRequiredService<IOptions<DatabaseSettings>>();
                                                return new MongoClient(settings.Value.ConnectionString);
                                            });

builder.Services.AddTransient<SensorDataContext>();

builder.Services.AddHostedService<DataWorker>();

builder.Services.AddAutoMapper(config =>
                                   config.AddMaps(Assembly.GetAssembly(typeof(SensorDataProfile))));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();