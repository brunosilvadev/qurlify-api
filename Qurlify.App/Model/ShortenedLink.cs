using System.Security.Permissions;
using System.Text.Json.Serialization;
using Qurlify.Utils;

namespace Qurlify.Model;

public class ShortenedLink(string longUrl)
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string LongUrl { get; set; } = longUrl;
    public string link { get; set; } = Formatter.PartitionKey();
    public int HitCounter { get; set; }

    public DateTime DateCreated { get; private set; } = DateTime.Now;
}