namespace InventorySystem.Shared.Helpers;

public static class SupplierCode
{
    public static string Generate()
    {
        long time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        return $"SP-{time}";
    }
}