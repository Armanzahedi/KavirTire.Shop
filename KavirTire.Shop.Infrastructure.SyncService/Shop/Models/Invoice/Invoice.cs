using System;
using System.Collections.Generic;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Common;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Entities;
using KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice.Enums;

namespace KavirTire.Shop.Infrastructure.SyncService.Shop.Models.Invoice
{
    public class Invoice : EntityBase
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public string PostalAddress { get; set; }
        public string MobilePhone { get; set; }
        public string Vehicle { get; set; }
        public string RegistrationPlate { get; set; }
        public decimal TotalPostCost { get; set; }
        public decimal TotalInventoryItemCost { get; set; }

        public Guid PriceListId { get; set; }
        public InvoiceStatus InvoiceStatus { get; set; } = InvoiceStatus.Draft;
        public InvoiceSyncStatus SyncStatus { get; set; } = InvoiceSyncStatus.OutOfSync;

        public DateTime CreateDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ExpirationDate { get; set; }

        public decimal TotalCost => TotalPostCost + TotalInventoryItemCost + Tax + Charges;
        public decimal Tax => Math.Ceiling(TotalInventoryItemCost * 6 / 100);
        public decimal Charges => Math.Ceiling(TotalInventoryItemCost * 3 / 100);

        public ICollection<InvoiceItem> InvoiceItems { get; set; }
        public ICollection<Payment.Payment> Payments { get; set; }
    }
}