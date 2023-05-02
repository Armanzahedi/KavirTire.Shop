using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Vehicles;
using KavirTire.Shop.Domain.VehicleTypes.Entities;

namespace KavirTire.Shop.Domain.VehicleTypes;

public class VehicleType : EntityBase<Guid>, IAggregateRoot
{
    public string Name { get; set; }

    private readonly List<Vehicle> _vehicles = new();
    public IEnumerable<Vehicle> Vehicles => _vehicles.AsReadOnly();
    
    private readonly List<VehicleTypeProduct> _vehicleTypeProducts = new();
    public IEnumerable<VehicleTypeProduct> VehicleTypeProducts => _vehicleTypeProducts.AsReadOnly();
}