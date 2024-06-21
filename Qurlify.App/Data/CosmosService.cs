using Microsoft.Azure.Cosmos;
using Qurlify.Model;
using Qurlify.Utils;

namespace Qurlify.Data;

public class CosmosService : ICosmosService
{
    private readonly CosmosClient _cosmosClient;
    private readonly Container _container;
    public CosmosService(CosmosClient cosmosClient, IConfiguration configuration)
    {
        _cosmosClient = cosmosClient;
        var databaseId = configuration["CosmosDb:DatabaseId"];
        var containerId = configuration["CosmosDb:ContainerId"];
        _container = _cosmosClient.GetContainer(databaseId, containerId);
    }

    public async Task<IEnumerable<ShortenedLink>> GetItemsAsync(string query)
    {
        var queryDefinition = new QueryDefinition(query);
        var iterator = _container.GetItemQueryIterator<ShortenedLink>(queryDefinition);
        var results = new List<ShortenedLink>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange([.. response]);
        }
        return results;
    }

    public async Task CreateLink(string longUrl)
    {
        var link = new ShortenedLink(longUrl);
        await _container.CreateItemAsync(link, new PartitionKey(link.link));
    }
    public async Task<IEnumerable<ShortenedLink>> GetItemsByPartitionKeyAsync(string partitionKey)
    {
        var queryDefinition = new QueryDefinition("SELECT * FROM c");
        var requestOptions = new QueryRequestOptions
        {
            PartitionKey = new PartitionKey(partitionKey),
            MaxItemCount = -1
        };
        var iterator = _container.GetItemQueryIterator<ShortenedLink>(queryDefinition, requestOptions: requestOptions);
        var results = new List<ShortenedLink>();

        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync();
            results.AddRange([.. response]);
        }

        return results;
    }
}

public interface ICosmosService
{
    Task<IEnumerable<ShortenedLink>> GetItemsAsync(string query);
    Task CreateLink(string longUrl);
    Task<IEnumerable<ShortenedLink>> GetItemsByPartitionKeyAsync(string partitionKey);
}