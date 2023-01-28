using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class UpgradeButton : MonoBehaviour
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

    [SerializeField]
    private Sprite maxButtonSprite;

    [Space]
    [SerializeField]
    private TextMeshProUGUI priceText;

    [Space]
    [SerializeField]
    private GameObject maxTextGO;

    [SerializeField]
    private GameObject titleTextGO;

    [SerializeField]
    private GameObject priceContainer;

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

    public void SetState(State state)
    {
        this.state = state;

        if (state == State.AVAILABLE)
        {
            this.button.interactable = true;
            this.buttonBackground.sprite = this.availableButtonSprite;

            this.priceContainer.SetActive(true);
            this.titleTextGO.SetActive(true);
            this.maxTextGO.SetActive(false);
        }
        else if (state == State.LOCKED)
        {
            this.button.interactable = false;
            this.buttonBackground.sprite = this.lockedButtonSprite;

            this.priceContainer.SetActive(true);
            this.titleTextGO.SetActive(true);
            this.maxTextGO.SetActive(false);
        }
        else if (state == State.MAX)
        {
            this.button.interactable = false;
            this.buttonBackground.sprite = this.maxButtonSprite;

            this.priceContainer.SetActive(false);
            this.titleTextGO.SetActive(false);
            this.maxTextGO.SetActive(true);
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
        MAX
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