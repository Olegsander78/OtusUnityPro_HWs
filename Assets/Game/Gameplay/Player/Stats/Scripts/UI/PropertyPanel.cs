using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertyPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _valueText;

    [SerializeField]
    private Image _iconImage;

    public void SetIcon(Sprite icon)
    {
        _iconImage.sprite = icon;
    }

    public void SetupValue(string text)
    {
        _valueText.text = text;
    }

    public void UpdateValue(string text)
    {
        _valueText.text = text;

        //Анимации:
        DOTween
            .Sequence()
            .Append(_valueText.DOColor(Color.red, 0.1f))
            .Append(_valueText.DOColor(Color.black, 0.1f))
            .Append(_valueText.DOColor(Color.red, 0.1f))
            .Append(_valueText.DOColor(Color.black, 0.1f));
    }
}
