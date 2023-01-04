using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public sealed class NumberItem : MonoBehaviour
{
    public int CurrentValue
    {
        get { return this.currentValue; }
    }

    [Header("Icon")]
    [SerializeField]
    private Image iconImage;

    [SerializeField]
    private RectTransform iconCenterPoint;

    [Header("Text")]
    [LabelText("Use Text Mesh Pro")]
    [SerializeField]
    private bool textMeshPro;

    [HideIf("textMeshPro")]
    [LabelText("Count Text")]
    [SerializeField]
    private Text numberText;

    [ShowIf("textMeshPro")]
    [LabelText("Count Text")]
    [SerializeField]
    private TextMeshProUGUI numberTMP;

    [PropertyOrder(-10)]
    [PropertySpace]
    [ReadOnly]
    [ShowInInspector]
    private int currentValue;

    public Vector3 GetIconCenter()
    {
        return this.iconCenterPoint.position;
    }

    public void SetIcon(Sprite icon)
    {
        this.iconImage.sprite = icon;
    }

    public void SetupNumber(int number)
    {
        this.currentValue = number;
        this.UpdateText();
    }

    public void UpdateNumber(int number)
    {
        this.currentValue = number;
        this.UpdateText();
    }

    public void IncrementNumber(int range)
    {
        range = Math.Max(0, range);
        this.currentValue += range;
        this.UpdateText();
        this.AnimateBounce();
    }

    private void AnimateBounce()
    {
        var transform = this.iconImage.transform;
        DOTween.Sequence()
            .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
            .Append(transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.15f));
    }

    public void DecrementNumber(int range)
    {
        range = Math.Max(0, range);
        this.currentValue -= range;
        this.UpdateText();
    }

    private void UpdateText()
    {
        var number = Math.Max(this.currentValue, 0);
        var numberString = number.ToString();

        if (this.textMeshPro)
            this.numberTMP.text = numberString;
        else
            this.numberText.text = numberString;
    }
}