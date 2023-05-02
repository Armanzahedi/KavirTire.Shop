using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;

namespace KavirTire.Shop.Domain.WebFiles;

public class WebFile : EntityBase<Guid>, IAggregateRoot
{
    public string PartialUrl { get; set; }
    public string Data { get; set; }
    public string MimeType { get; set; }
}