using System.ComponentModel.DataAnnotations;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Locations;
using KavirTire.Shop.Domain.Vehicles;

namespace KavirTire.Shop.Domain.Customers;

public class Customer : EntityBase<Guid>, IAggregateRoot
{

    public Guid? ProvinceId { get; set; }
    public Location? Province { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? NationalId { get; set; }
    public string? PostalCode { get; set; }
    public string? PostalAddress { get; set; }
    public string? MobilePhone { get; set; }
    public bool IsApprovedForPurchase { get; set; }
    
    public long? CrmRowVersion { get; set; }
        
    private readonly List<Vehicle> _vehicles = new();
    public IEnumerable<Vehicle> Vehicles => _vehicles.AsReadOnly();

    
    private readonly List<OrderHistory.OrderHistory> _orderHistory = new();
    public IEnumerable<OrderHistory.OrderHistory> OrderHistory => _orderHistory.AsReadOnly();


    public Vehicle? Vehicle => Vehicles.FirstOrDefault();
    public int PreviousPurchaseCount()
    {
        return OrderHistory.Sum(x => x.TotalQuantity);
    }

}