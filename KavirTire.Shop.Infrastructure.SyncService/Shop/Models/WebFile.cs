using System.ComponentModel.DataAnnotations.Schema;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models
{
    [Table("WebFiles")]
    public class WebFile : EntityBase
    {
        public string PartialUrl { get; set; }
        public string Data { get; set; }
        public string MimeType { get; set; }
    }
}