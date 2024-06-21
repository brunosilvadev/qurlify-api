using Qurlify.Model;

public class LinkEndpoint : IEndpoint
{
    public void RegisterRoutes(IEndpointRouteBuilder app)
    {

    }

    public IEnumerable<Link> GetAllLinks()
    {
        return new List<Link>();
    }
}