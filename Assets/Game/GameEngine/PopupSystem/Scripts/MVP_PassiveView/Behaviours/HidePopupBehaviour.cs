using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class HidePopupBehaviour : MonoBehaviour, 
    IGameConstructElement
{
    [SerializeField]
    private PopupName popupName;

    private PopupManager popupManager;

    [Button]
    public void HidePopup()
    {
        this.popupManager.HidePopup(this.popupName);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.popupManager = context.GetService<PopupManager>();
    }
}