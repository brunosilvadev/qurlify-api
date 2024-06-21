namespace Qurlify.Model;

public class Link
{
    public string LongUrl { get; set; } = "";
    public string ShortUrl { get; set; } = "";
    public string MonthYear { get; set; } = "";
    public int HitCounter { get; set; }
}