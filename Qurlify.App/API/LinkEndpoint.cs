using Qurlify.Data;
using Qurlify.Model;

public class LinkEndpoint(ICosmosService cosmosService) : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/all", GetAllLinks);
        app.MapGet("/s", GetLink);
        app.MapGet("/c", CreateLink);        
    }

    public async Task<IEnumerable<ShortenedLink>> GetAllLinks() =>
        await cosmosService.GetItemsAsync("SELECT * FROM c");

    public async Task<IEnumerable<ShortenedLink>> GetLink(string url) =>
        await cosmosService.GetItemsByPartitionKeyAsync(url);

    public async Task CreateLink(string longUrl) =>
        await cosmosService.CreateLink(longUrl);

}

public interface IEndpoint
{
    void RegisterRoutes(IEndpointRouteBuilder app);
}
