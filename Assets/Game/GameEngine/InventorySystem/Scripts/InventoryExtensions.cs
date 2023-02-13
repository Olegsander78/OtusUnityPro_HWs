
public static class InventoryExtensions
{
    public static bool FlagsExists(this InventoryItem it, InventoryItemFlags flags)
    {
        return (it.Flags & flags) == flags;
    }

    public static bool FlagsExists(this InventoryItemConfig it, InventoryItemFlags flags)
    {
        return (it.Flags & flags) == flags;
    }
}