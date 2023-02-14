using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;


public sealed class BoosterListViewAdapter : MonoBehaviour,
    IGameConstructElement,
    IGameReadyElement,
    IGameFinishElement
{
    [SerializeField]
    [FormerlySerializedAs("viewList")]
    private BoosterListView listView;

    private BoostersManager boostersManager;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.boostersManager = context.GetService<BoostersManager>();
    }

    void IGameReadyElement.ReadyGame()
    {
        this.boostersManager.OnBoosterStarted += this.OnBoosterStarted;
        this.boostersManager.OnBoosterFinished += this.OnBoosterFinished;
    }

    void IGameFinishElement.FinishGame()
    {
        this.boostersManager.OnBoosterStarted -= this.OnBoosterStarted;
        this.boostersManager.OnBoosterFinished -= this.OnBoosterFinished;
    }

    private void OnBoosterStarted(Booster booster)
    {
        this.listView.AddBooster(booster);
    }

    private void OnBoosterFinished(Booster booster)
    {
        this.listView.RemoveBooster(booster);
    }
}