using UnityEngine;
using UnityEngine.UI;


public sealed class InfoWidgetTwoInput : MonoBehaviour
{
    public ProgressBar ProgressBar
    {
        get { return this.progressBar; }
    }

    [SerializeField]
    private GameObject root;

    [SerializeField]
    private Image inputImageOne;

    [SerializeField]
    private Image inputImageTwo;

    [SerializeField]
    private Image outputImage;

    [SerializeField]
    private ProgressBar progressBar;

    public void SetVisible(bool isVisible)
    {
        this.root.SetActive(isVisible);
    }

    public void SetInputIcon(Sprite iconOne, Sprite iconTwo)
    {
        this.inputImageOne.sprite = iconOne;
        this.inputImageTwo.sprite = iconTwo;
    }

    public void SetOutputIcon(Sprite icon)
    {
        this.outputImage.sprite = icon;
    }
}