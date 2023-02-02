using UnityEngine;


public sealed class ChestsAssetSupplier : IAppConfigsLoader
{
    private ChestCatalog _catalog;

    public ChestConfig GetChest(string id)
    {
        return _catalog.FindChest(id);
    }

    public ChestConfig[] GetAllChests()
    {
        return _catalog.GetAllChests();
    }

    void IAppConfigsLoader.LoadConfigs()
    {
        _catalog = Resources.Load<ChestCatalog>("ChestsCatalog");
        //_catalog = Resources.Load<ChestCatalog>(BoosterExtensions.BOOSTER_CATALOG_RESOURCE_PATH);
    }
}