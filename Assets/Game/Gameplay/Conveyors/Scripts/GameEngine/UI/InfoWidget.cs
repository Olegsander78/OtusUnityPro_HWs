using UnityEngine;
using UnityEngine.UI;


public sealed class InfoWidget : MonoBehaviour
{
    public ProgressBar ProgressBar
    {
        get { return this.progressBar; }
    }

    [SerializeField]
    private GameObject root;

    [SerializeField]
    private Image inputImage;

    [SerializeField]
    private Image outputImage;

    [SerializeField]
    private ProgressBar progressBar;

    public void SetVisible(bool isVisible)
    {
        this.root.SetActive(isVisible);
    }

    public void SetInputIcon(Sprite icon)
    {
        this.inputImage.sprite = icon;
    }

    public void SetOutputIcon(Sprite icon)
    {
        this.outputImage.sprite = icon;
    }
}