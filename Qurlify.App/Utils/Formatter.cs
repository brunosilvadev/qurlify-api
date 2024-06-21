namespace Qurlify.Utils;

public static class Formatter
{
    private static readonly Random _random = new();

    public static string PartitionKey()
    {
        return _random.Next(int.MaxValue).ToString("x");
    }
}