namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Models.Order
{
    public enum OrderStatusReason
    {
        New = 1,
        Pending = 2,
        Proceed = 276160001,
        ReadyForSend = 276160000,
        Sent = 276160002,
        RefundRequested = 276160003
    }
}