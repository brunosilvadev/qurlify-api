using Qurlify.Utils;

namespace Qurlify.Model;

public class ShortenedLink
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string LongUrl { get; set; } = "";
    public string link { get; set; } = Formatter.PartitionKey();
    public int HitCounter { get; set; }
    public DateTime DateCreated { get; private set; } = DateTime.Now;
    public string Comment { get; set; } = "";

    public ShortenedLink(CreateLinkRequest request)
    {
        LongUrl = request.LongUrl;
        Comment = request.Comment;
    }

    public ShortenedLink() { }
}