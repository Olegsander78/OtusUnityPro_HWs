using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class HidePopupBehaviour : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private PopupName popupName;

    private PopupManager popupManager;

    [Button]
    public void HidePopup()
    {
        this.popupManager.HidePopup(this.popupName);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.popupManager = context.GetService<PopupManager>();
    }
}