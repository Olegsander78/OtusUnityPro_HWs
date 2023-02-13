using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class CraftButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [Space]
    [SerializeField]
    private Image buttonBackground;

    [SerializeField]
    private Sprite availableButtonSprite;

    [SerializeField]
    private Sprite unavailableButtonSprite;

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
            this.buttonBackground.sprite = this.unavailableButtonSprite;
        }
        else
        {
            throw new Exception($"Undefined button state {state}!");
        }
    }

    public enum State
    {
        AVAILABLE,
        LOCKED
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