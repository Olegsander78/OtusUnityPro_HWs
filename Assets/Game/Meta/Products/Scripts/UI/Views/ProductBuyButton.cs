using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class ProductBuyButton : MonoBehaviour
{
    public event UnityAction OnClicked
    {
        add { this.button.onClick.AddListener(value); }
        remove { this.button.onClick.RemoveListener(value); }
    }

    public PricePanel PriceItem1
    {
        get { return this.priceItem1; }
    }

    public PricePanel PriceItem2
    {
        get { return this.priceItem2; }
    }

    [SerializeField]
    private RectTransform mainTransform;

    [SerializeField]
    private Button button;

    [Space]
    [SerializeField]
    private Image buttonBackground;

    [SerializeField]
    private Sprite availableButtonSprite;

    [SerializeField]
    private Sprite lockedButtonSprite;

    [SerializeField]
    private PricePanel priceItem1;

    [SerializeField]
    private PricePanel priceItem2;

    [Space]
    [SerializeField]
    private State state;

    public void SetPriceSize1()
    {
        this.priceItem1.gameObject.SetActive(true);
        this.priceItem2.gameObject.SetActive(false);
        this.mainTransform.sizeDelta = new Vector2(this.mainTransform.sizeDelta.x, 140);
    }

    public void SetPriceSize2()
    {
        this.priceItem1.gameObject.SetActive(true);
        this.priceItem2.gameObject.SetActive(true);
        this.mainTransform.sizeDelta = new Vector2(this.mainTransform.sizeDelta.x, 200);
    }

    public void SetState(State state)
    {
        this.state = state;

        if (state == State.AVAILABLE)
        {
            this.button.interactable = true;
            this.buttonBackground.sprite = this.availableButtonSprite;
        }
        else if (state == State.LOCKED)
        {
            this.button.interactable = false;
            this.buttonBackground.sprite = this.lockedButtonSprite;
        }
        else
        {
            throw new Exception($"Undefined button state {state}!");
        }
    }

    public enum State
    {
        AVAILABLE,
        LOCKED,
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        try
        {
            this.SetState(this.state);
        }
        catch (Exception)
        {
            // ignored
        }
    }
#endif
}