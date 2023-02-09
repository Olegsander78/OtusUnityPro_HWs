using Services;
using UnityEngine;

public sealed class ChestsMediator :
    IGameLoadDataListener,
    IGameSaveDataListener
{
    [ServiceInject]
    private ChestsRepository _repository;

    [ServiceInject]
    private ChestsAssetSupplier _assetSupplier;
    
    private ChestsManager _chestsManager;

    [ServiceInject]
    public void Construct(ChestsRepository repository, ChestsAssetSupplier chestsAssetSupplier)
    {
        _repository = repository;
        _assetSupplier = chestsAssetSupplier;
    }

    void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    {
        _chestsManager = gameManager.GetService<ChestsManager>();

        if (_repository.LoadChests(out var chestsData))
        {
            SetupChests(chestsData);
        }
    }

    void IGameSaveDataListener.OnSaveData(GameSaveReason reason)
    {
        SaveChests();
    }

    private void SetupChests(ChestData[] chestsData)
    {
        Debug.Log("START SETUP CHEST");

        for (int i = 0, count = chestsData.Length; i < count; i++)
        {
            var data = chestsData[i];
            var config = _assetSupplier.GetChest(data.id);
            Debug.Log($"Chest Id - {config.Id}. ¹ Chests - {chestsData.Length}");
            var chest = _chestsManager.InstallChest(config);
            chest.Start();
            
            //_chestsManager.ActivateChest(chest.Config);
            chest.RemainingSeconds = data.remainingTime;
        }
    }

    private void SaveChests()
    {
        var chests = _chestsManager.GetActiveChests();
        var count = chests.Length;
        var chestsData = new ChestData[count];

        for (var i = 0; i < count; i++)
        {
            var chest = chests[i];
            var data = new ChestData
            {
                id = chest.Id,
                remainingTime = chest.RemainingSeconds
            };

            chestsData[i] = data;
        }

        _repository.SaveChests(chestsData);
    }

    //void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    //{
    //    _chestsManager = gameManager.GetService<ChestsManager>();

    //    if (_repository.LoadChests(out var chestsData))
    //    {
    //        SetupChests(chestsData);
    //    }
    //}
}