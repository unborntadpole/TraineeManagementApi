namespace TraineeManagementApi.db;
using StackExchange.Redis;
using System.Text.Json;

public class RedisCacheHelper
{
    private static readonly ConfigurationOptions conf = new ConfigurationOptions {
        EndPoints = { "localhost:6379" },
        // User = "samriddh",
        Password = "helloworld"
    };
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(conf);
    // Source - https://stackoverflow.com/a/27138710
// Posted by pranav rastogi, modified by community. See post 'Timeline' for change history
// Retrieved 2026-06-22, License - CC BY-SA 4.0

    // private static ConnectionMultiplexer redis = 
    //     ConnectionMultiplexer.Connect("http://localhost:6379,password=helloworld");//,ConnectTimeout=10000");
   

    private static IDatabase db = redis.GetDatabase();
    public static async Task SetObjectAsync<T>(string key, T value, double expiryInMinutes, ILogger logger)
    {
        string json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json, TimeSpan.FromMinutes(expiryInMinutes));
        logger.LogInformation($"Entry serialized in Redis with key {key}");
    }

    public static async Task<T?> GetObjectAsync<T>(string key, ILogger logger)
    {
        string? value = await db.StringGetAsync(key);
        if (value == null) 
        {
            logger.LogWarning($"Entry {key} not found in Redis");
            return default;
        }
        logger.LogInformation($"Found entry with key {key}");
        return JsonSerializer.Deserialize<T>(value);
    }

    public static async Task KeyDelete(string key, ILogger logger)
    {
        await db.KeyDeleteAsync(key);
        logger.LogInformation($"Entry with key {key} deleted in redis");
    }
}