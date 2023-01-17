using UnityEngine;


[AddComponentMenu("Audio/UISound/UI Sound Component")]
public sealed class UISoundComponent : MonoBehaviour
{
    public void PlayClick()
    {
        UISoundManager.PlaySound(UISoundType.CLICK);
    }

    public void PlayError()
    {
        UISoundManager.PlaySound(UISoundType.ERROR);
    }

    public void PlayAccept()
    {
        UISoundManager.PlaySound(UISoundType.ACCEPT);
    }

    public void PlayClose()
    {
        UISoundManager.PlaySound(UISoundType.CLOSE);
    }

    public void PlayBuy()
    {
        UISoundManager.PlaySound(UISoundType.BUY);
    }

    public void PlayShowPopup()
    {
        UISoundManager.PlaySound(UISoundType.SHOW_POPUP);
    }

    public void PlayIncome()
    {
        UISoundManager.PlaySound(UISoundType.INCOME);
    }
}