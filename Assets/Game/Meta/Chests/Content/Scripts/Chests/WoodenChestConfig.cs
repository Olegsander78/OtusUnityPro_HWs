using UnityEngine;


[CreateAssetMenu(
    fileName = "Wooden Chest",
    menuName = "Meta/Chests/New Wooden Chest Config"
)]
public sealed class WoodenChestConfig : ChestConfig
{
    public override Chest InstantiateChest(MonoBehaviour context)
    {
        return new WoodenChest(this, context);
    }
}