using GameElements;
using UnityEngine;


public sealed class ChestsInstaller : MonoBehaviour, IGameInitElement
{   
    [SerializeField]
    private WoodenChestConfig _woodenChest;

    [SerializeField]
    private SteelChestConfig _steelChest;

    [SerializeField]
    private GoldChestConfig _goldenChest;

    void IGameInitElement.InitGame(IGameContext context)
    {        
        var questsManager = context.GetService<ChestsManager>();

        if (!questsManager.IsChestExists(ChestType.WOODEN_CHEST))
        {
            questsManager.InstallChest(_woodenChest);
        }

        if (!questsManager.IsChestExists(ChestType.STEEL_CHEST))
        {
            questsManager.InstallChest(_steelChest);
        }

        if (!questsManager.IsChestExists(ChestType.GOLD_CHEST))
        {
            questsManager.InstallChest(_goldenChest);
        }
    }
}