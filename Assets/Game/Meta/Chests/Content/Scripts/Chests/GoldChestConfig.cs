using UnityEngine;


[CreateAssetMenu(
    fileName = "Gold Chest",
    menuName = "Meta/Chests/New Gold Chest Config"
)]
public sealed class GoldChestConfig : ChestConfig
{
    public override Chest InstantiateChest(MonoBehaviour context)
    {
        return new GoldChest(this, context);
    }
}