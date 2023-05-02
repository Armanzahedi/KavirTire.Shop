namespace KavirTire.Shop.Infrastructure.PaymentService.SamanKish.Models;

public enum SepResponseStatus
{
    CanceledByUser =1,
    OK = 2,
    Failed = 3,
    SessionIsNull = 4,
    InvalidParameters = 5,
    MerchantIpAddressIsInvalid = 8,
    TokenNotFound = 10,
    TokenRequired = 11,
    TerminalNotFound = 12
}