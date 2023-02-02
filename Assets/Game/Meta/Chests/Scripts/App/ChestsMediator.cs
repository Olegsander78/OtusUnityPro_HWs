using Services;


public sealed class ChestsMediator :
    IGameSetupListener,
    IGameSaveListener
{
    [Inject]
    private ChestsRepository _repository;

    [Inject]
    private ChestsAssetSupplier _assetSupplier;

    private ChestsManager _chestsManager;

    void IGameSetupListener.OnSetupGame(GameManager gameManager)
    {
        _chestsManager = gameManager.GetService<ChestsManager>();
        if (_repository.LoadChests(out var chestsData))
        {
            SetupChests(chestsData);
        }
    }

    void IGameSaveListener.OnSaveGame(GameSaveReason reason)
    {
        SaveChests();
    }

    private void SetupChests(ChestData[] chestsData)
    {
        for (int i = 0, count = chestsData.Length; i < count; i++)
        {
            var data = chestsData[i];
            var config = _assetSupplier.GetChest(data.id);
            var chest = _chestsManager.InstallChest(config);
            //_chestsManager.ActivateChest(chest.Config);
            chest.Start();
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
}