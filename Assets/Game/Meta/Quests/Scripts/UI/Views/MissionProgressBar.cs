using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class MissionProgressBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Color completeTextColor;

    [SerializeField]
    private Color processingTextColor;

    [Space]
    [SerializeField]
    private Image fill;

    [SerializeField]
    private Color completeFillColor;

    [SerializeField]
    private Color progressFillColor;

    public void SetProgress(float progress, string text)
    {
        this.fill.fillAmount = progress;
        this.text.text = text;

        if (progress >= 1)
        {
            this.text.color = this.completeTextColor;
            this.fill.color = this.completeFillColor;
        }
        else
        {
            this.text.color = this.processingTextColor;
            this.fill.color = this.progressFillColor;
        }
    }
}