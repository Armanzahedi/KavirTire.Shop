namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Repository
{
    public class RepositoryResult
    {
        public bool IsSuccessful { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
