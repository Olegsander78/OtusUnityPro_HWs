using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class BuyButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [Space]
    [SerializeField]
    private Image buttonBackground;

    [SerializeField]
    private Sprite availableButtonSprite;

    [SerializeField]
    private Sprite lockedButtonSprite;

    [Space]
    [SerializeField]
    private TextMeshProUGUI priceText;

    [SerializeField]
    private Image priceIcon;

    [Space]
    [SerializeField]
    private State state;

    public void AddListener(UnityAction action)
    {
        this.button.onClick.AddListener(action);
    }

    public void RemoveListener(UnityAction action)
    {
        this.button.onClick.RemoveListener(action);
    }

    public void SetPrice(string price)
    {
        this.priceText.text = price;
    }

    public void SetIcon(Sprite icon)
    {
        this.priceIcon.sprite = icon;
    }

    public void SetAvailable(bool isAvailable)
    {
        var state = isAvailable ? State.AVAILABLE : State.LOCKED;
        this.SetState(state);
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
            throw new Exception($"Undefined _buttonReward state {state}!");
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
