using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ShowPopupBehaviour : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private PopupName popupName;

    private PopupManager popupManager;

    [Button]
    public void ShowPopup()
    {
        this.popupManager.ShowPopup(this.popupName);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.popupManager = context.GetService<PopupManager>();
    }
}