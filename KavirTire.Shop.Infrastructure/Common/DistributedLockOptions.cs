namespace KavirTire.Shop.Infrastructure.Common;

public class DistributedLockOptions
{
    public const string DistributedLock = "DistributedLock";
    public string Expiry { get; set; } = string.Empty;
    public string Wait { get; set; } = string.Empty;
    public string Retry { get; set; } = string.Empty;
    
}