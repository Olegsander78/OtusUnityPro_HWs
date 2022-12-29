using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class HitPointsBar : MonoBehaviour
{
    [SerializeField]
    private GameObject root;

    [SerializeField]
    private Image progressBar;

    [SerializeField]
    private TextMeshProUGUI text;

    public void SetHitPoints(int current, int max)
    {
        this.text.text = $"{current}/{max}";
        this.progressBar.fillAmount = (float)current / max;
    }

    public void SetVisible(bool isVisible)
    {
        this.root.SetActive(isVisible);
    }

    public void Show()
    {
        this.root.SetActive(true);
    }

    public void Hide()
    {
        this.root.SetActive(false);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        this.root = this.gameObject;
    }
#endif
}