using UnityEngine;


[CreateAssetMenu(
    fileName = "Steel Chest",
    menuName = "Meta/Chests/New Steel Chest Config"
)]
public sealed class SteelChestConfig : ChestConfig
{
    public override Chest InstantiateChest(MonoBehaviour context)
    {
        return new SteelChest(this, context);
    }
}