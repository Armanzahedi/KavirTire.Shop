using System.ServiceModel;
using Microsoft.Xrm.Sdk;

namespace KavirTire.Shop.Infrastructure.SyncService.CRM.Common.Helpers
{
    public static class CrmExceptionHelper
    {
        public static bool IsRecordNotFoundException(this FaultException<OrganizationServiceFault> ex)
        {
            return ex != null && ex.Detail.ErrorCode == -2147220969;
        }
        public static bool IsRecordExistsException(this FaultException<OrganizationServiceFault> ex)
        {
            return ex != null && ex.Detail.ErrorCode == -2147088238;
        }
    }
}
