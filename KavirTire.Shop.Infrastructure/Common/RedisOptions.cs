namespace KavirTire.Shop.Infrastructure.Common;

public class RedisOptions 
{
    public const string Redis = "Redis";
    public string Address { get; set; } = string.Empty;
    public string Port { get; set; } = string.Empty;
}