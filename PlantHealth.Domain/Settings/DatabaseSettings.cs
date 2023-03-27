namespace PlantHealth.Domain.Settings;

public class DatabaseSettings
{
    public string ConnectionString
    {
        get;
        set;
    } = null!;

    public string DatabaseName
    {
        get;
        set;
    } = null!;

    public string SensorDataCollectionName
    {
        get;
        set;
    } = null!;
}