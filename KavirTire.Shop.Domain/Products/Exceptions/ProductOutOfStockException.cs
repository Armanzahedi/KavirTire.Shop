namespace KavirTire.Shop.Domain.Products.Exceptions;

public class ProductOutOfStockException : Exception
{
    public ProductOutOfStockException()
        : base()
    {
    }

    public ProductOutOfStockException(string productName)
        : base($"موجودی محصول {productName} به پایان رسیده")
    {
    }

    public ProductOutOfStockException(string productName, Exception innerException)
        : base($"موجودی محصول {productName} به پایان رسیده", innerException)
    {
    }

  
}