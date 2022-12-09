using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


public sealed class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [FormerlySerializedAs("progress")]
    [SerializeField]
    private Image fillImage;

    [Space]
    [SerializeField]
    private bool hasMask;

    [ShowIf("hasMask")]
    [SerializeField]
    private Image maskImage;

    public void SetVisible(bool isVisible)
    {
        this.root.SetActive(isVisible);
    }

    public void SetProgress(float progress)
    {
        if (this.hasMask)
        {
            this.maskImage.fillAmount = progress;
        }
        else
        {
            this.fillImage.fillAmount = progress;
        }
    }

    public void SetColor(Color color)
    {
        this.fillImage.color = color;
    }
}