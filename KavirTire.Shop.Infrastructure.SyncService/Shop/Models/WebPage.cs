using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("WebPages")]
    public class WebPage : EntityBase
    {
        public string Key { get; set; }
        public string Data { get; set; }
    }
}