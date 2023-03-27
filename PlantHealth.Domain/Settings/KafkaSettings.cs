namespace PlantHealth.Domain.Settings;

public record KafkaSettings
{
    public string BootstrapServers
    {
        get;
        set;
    } = null!;

    public string Topic
    {
        get;
        set;
    } = null!;

    public string GroupId
    {
        get;
        set;
    } = null!;
}