using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;

namespace KavirTire.Shop.Domain.WebPages;

public class WebPage : EntityBase<Guid>, IAggregateRoot
{
    public string Key { get; set; }
    public string Data { get; set; }
}