using Moq;
using Qurlify.Data;
using Qurlify.Model;

namespace Qurlify.Tests;

public class LinkEndpointTests
{
    private readonly Mock<ICosmosService> _mockCosmosService;
    private readonly LinkEndpoint _linkEndpoint;

    public LinkEndpointTests()
    {
        _mockCosmosService = new Mock<ICosmosService>();
        _linkEndpoint = new LinkEndpoint(_mockCosmosService.Object);
    }

    [Fact]
    public async Task GetAllLinks_ShouldReturnLinks()
    {
        // Arrange
        var expectedLinks = new List<ShortenedLink> 
        {
            new() { LongUrl = "https://example.com", link = "abc123" },
            new() { LongUrl = "https://test.com", link = "def456" }
        };
        
        _mockCosmosService.Setup(x => x.GetItemsAsync(It.IsAny<string>()))
            .ReturnsAsync(expectedLinks);

        // Act
        var result = await _linkEndpoint.GetAllLinks();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLinks.Count, result.Count());
        _mockCosmosService.Verify(x => x.GetItemsAsync("SELECT * FROM c"), Times.Once);
    }

    [Fact]
    public async Task GetLink_ShouldReturnLinkByUrl()
    {
        // Arrange
        var url = "abc123";
        var expectedLink = new ShortenedLink { LongUrl = "https://example.com", link = url };
        
        _mockCosmosService.Setup(x => x.GetItemsByPartitionKeyAsync(url))
            .ReturnsAsync(expectedLink);

        // Act
        var result = await _linkEndpoint.GetLink(url);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedLink.LongUrl, result.LongUrl);
        Assert.Equal(url, result.link);
        _mockCosmosService.Verify(x => x.GetItemsByPartitionKeyAsync(url), Times.Once);
    }

    [Fact]
    public async Task ShortenLink_ShouldCreateAndReturnShortLink()
    {
        // Arrange
        var request = new CreateLinkRequest 
        { 
            LongUrl = "https://example.com/very/long/url/that/needs/shortening",
            Comment = "Test URL"
        };
        var expectedShortLink = "abc123";
        
        _mockCosmosService.Setup(x => x.ShortenLink(request))
            .ReturnsAsync(expectedShortLink);

        // Act
        var result = await _linkEndpoint.ShortenLink(request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedShortLink, result);
        _mockCosmosService.Verify(x => x.ShortenLink(request), Times.Once);
    }
}
