namespace KavirTire.Shop.Domain.Vehicles.Enums;

public static class RegistrationPlateCharacterExtensions
{
    public static readonly Dictionary<int, string> RegistrationPlateCharacterDic;

    static RegistrationPlateCharacterExtensions()
    {
        RegistrationPlateCharacterDic = new Dictionary<int, string>
        {
            { 276160000, "الف" },
            { 276160001, "ب" },
            { 276160002, "پ" },
            { 276160003, "ت" },
            { 276160004, "ج" },
            { 276160005, "د" },
            { 276160006, "س" },
            { 276160007, "ص" },
            { 276160008, "ط" },
            { 276160009, "ژ" },
            { 276160010, "ق" },
            { 276160012, "ک" },
            { 276160013, "ع" },
            { 276160014, "ل" },
            { 276160015, "م" },
            { 276160016, "ن" },
            { 276160011, "و" },
            { 276160017, "ه" },
            { 276160018, "ی" },
            { 276160019, ".." },
        };
    }

}