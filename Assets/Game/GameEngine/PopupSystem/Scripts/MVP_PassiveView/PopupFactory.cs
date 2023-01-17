using GameElements;
using UIFrames.Unity;
using UnityEngine;


public sealed class PopupFactory : UnityFrameFactory<PopupName, UnityFrame>, IGameAttachElement
{
    private IGameContext gameContext;

    [SerializeField]
    private PopupCatalog catalog;

    protected override UnityFrame GetPrefab(PopupName key)
    {
        return this.catalog.LoadPrefab(key);
    }

    protected override void OnFrameCreated(UnityFrame popup)
    {
        if (popup.TryGetComponent(out IGameElement gameElement))
        {
            this.gameContext.RegisterElement(gameElement);
        }
    }

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        this.gameContext = context;
    }
}