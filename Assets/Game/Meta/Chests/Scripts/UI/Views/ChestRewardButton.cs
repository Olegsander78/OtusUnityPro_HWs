using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public sealed class ChestRewardButton : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [Space]
    [SerializeField]
    private Image buttonBackground;

    [SerializeField]
    private Sprite availableBackground;

    [SerializeField]
    private Sprite unavailableBackground;

    [Space]
    [SerializeField]
    private GameObject processingText;

    [SerializeField]
    private GameObject getText;

    //[SerializeField]
    //private TextMeshProUGUI rewardText;

    //[SerializeField]
    //private TextMeshProUGUI rewardExpText;

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

    //public void SetMoneyReward(string reward)
    //{
    //    this.rewardText.text = reward;
    //}

    //public void SetExpReward(string reward)
    //{
    //    this.rewardExpText.text = reward;
    //}

    public void SetState(State state)
    {
        this.state = state;

        if (state == State.COMPLETE)
        {
            this.button.interactable = true;
            this.buttonBackground.sprite = this.availableBackground;
            this.getText.SetActive(true);
            this.processingText.SetActive(false);
        }
        else if (state == State.PROCESSING)
        {
            this.button.interactable = false;
            this.buttonBackground.sprite = this.unavailableBackground;
            this.getText.SetActive(false);
            this.processingText.SetActive(true);
        }
        else
        {
            throw new Exception($"Undefined _buttonReward state {state}!");
        }
    }

    public enum State
    {
        COMPLETE = 0,
        PROCESSING = 1
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