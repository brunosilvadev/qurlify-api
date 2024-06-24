using Microsoft.AspNetCore.Mvc;
using Qurlify.Data;
using Qurlify.Model;

public class LinkEndpoint(ICosmosService cosmosService) : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/all", GetAllLinks);
        app.MapGet("/s", GetLink);
        app.MapPost("/c", ShortenLink);        
    }

    public async Task<IEnumerable<ShortenedLink>> GetAllLinks() =>
        await cosmosService.GetItemsAsync("SELECT * FROM c");

    public async Task<ShortenedLink> GetLink(string url) =>
        await cosmosService.GetItemsByPartitionKeyAsync(url);

    public async Task<string> ShortenLink([FromBody] CreateLinkRequest request) =>
        await cosmosService.ShortenLink(request);

}

public interface IEndpoint
{
    void RegisterRoutes(IEndpointRouteBuilder app);
}
