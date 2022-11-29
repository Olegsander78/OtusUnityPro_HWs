using TMPro;
using UnityEngine;


public sealed class PopupInfoView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI descriptionText;

    public void Show(object args)
    {
        if (args is PopupArgs infoArgs)
        {
            this.titleText.text = infoArgs.title;
            this.descriptionText.text = infoArgs.description;
        }
    }
}
