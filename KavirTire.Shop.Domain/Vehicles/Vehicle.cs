using Ardalis.GuardClauses;
using KavirTire.Shop.Domain.Common;
using KavirTire.Shop.Domain.Common.Interfaces;
using KavirTire.Shop.Domain.Customers;
using KavirTire.Shop.Domain.Vehicles.Enums;
using KavirTire.Shop.Domain.VehicleTypes;

namespace KavirTire.Shop.Domain.Vehicles;

public class Vehicle : EntityBase<Guid>, IAggregateRoot
{
    public Guid? VehicleTypeId { get; set; }
    
    public Guid? CustomerId { get; set; }
    
    
    public int? RegistrationPlateNumberLeft { get; set; }
    public int? RegistrationPlateNumberMiddle { get; set; }
    public int? RegistrationPlateNumberRight { get; set; }
    public int? RegistrationPlateCharacter { get; set; }
    
    
    private string? GetRegistrationPlateCharacter()
    {
        if (RegistrationPlateCharacter == null)
            throw new InvalidCastException("Registration plate character is undefined.");
        
        return RegistrationPlateCharacterExtensions.RegistrationPlateCharacterDic.TryGetValue(RegistrationPlateCharacter.Value, out var val) ? val : default;
    }

    public string? RegistrationPlate => $"{RegistrationPlateNumberLeft} {GetRegistrationPlateCharacter()} {RegistrationPlateNumberMiddle} ایران {RegistrationPlateNumberRight}";
}