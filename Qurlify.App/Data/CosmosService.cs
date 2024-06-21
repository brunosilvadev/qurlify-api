using Microsoft.Azure.Cosmos;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;
    public CosmosDataProvider(CosmosClient cosmosClient, IConfiguration configuration)
    {
        _cosmosClient = cosmosClient;
        var databaseId = configuration["CosmosDb:DatabaseId"];
        var containerId = configuration["CosmosDb:ContainerId"];
        _container = _cosmosClient.GetContainer(databaseId, containerId);
    }

    public async Task<IEnumerable<T>> GetItemsAsync<T>(string query)
    {
        var queryDefinition = new QueryDefinition(query);
        var iterator = _container.GetItemQueryIterator<T>(queryDefinition);
        var results = new List<T>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange(response.ToList());
        }
        return results;
    }

    public async Task AddItemAsync<T>(T item)
    {
        await _container.CreateItemAsync(item);
    }
}

public interface ICosmosService
{
    Task<IEnumerable<T>> GetItemsAsync<T>(string query);
    Task AddItemAsync<T>(T item);
}