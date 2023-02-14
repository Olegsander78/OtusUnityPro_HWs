using System;
using System.Collections;
using System.Collections.Generic;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class BoostersManager :
    IGameStartElement,
    IGameFinishElement
{
    public event Action<Booster> OnBoosterLaunched;

    public event Action<Booster> OnBoosterStarted;

    public event Action<Booster> OnBoosterFinished;

    private BoosterFactory factory;

    private MonoBehaviour monoContext;

    [PropertySpace(8), ReadOnly, ShowInInspector]
    private readonly List<Booster> currentBoosters = new();

    [GameInject]
    public void Construct(MonoBehaviour monoContext, BoosterFactory factory)
    {
        this.factory = factory;
        this.monoContext = monoContext;
    }

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void LaunchBooster(BoosterConfig config)
    {
        var booster = this.factory.CreateBooster(config);
        booster.OnEnded += this.OnEndBooster;

        this.currentBoosters.Add(booster);

        booster.Start();
        this.OnBoosterStarted?.Invoke(booster);
        this.OnBoosterLaunched?.Invoke(booster);
    }

    public Booster SetupBooster(BoosterConfig config)
    {
        var booster = this.factory.CreateBooster(config);
        booster.OnEnded += this.OnEndBooster;

        this.currentBoosters.Add(booster);
        return booster;
    }

    public Booster[] GetActiveBoosters()
    {
        return this.currentBoosters.ToArray();
    }

    void IGameStartElement.StartGame()
    {
        this.StartAllBoosters();
    }

    void IGameFinishElement.FinishGame()
    {
        this.StopAllBoosters();
    }

    private void StartAllBoosters()
    {
        for (int i = 0, count = this.currentBoosters.Count; i < count; i++)
        {
            var booster = this.currentBoosters[i];
            if (booster.IsActive)
            {
                continue;
            }

            booster.Start();
            this.OnBoosterStarted?.Invoke(booster);
        }
    }

    private void StopAllBoosters()
    {
        for (int i = 0, count = this.currentBoosters.Count; i < count; i++)
        {
            var booster = this.currentBoosters[i];
            if (!booster.IsActive)
            {
                continue;
            }

            booster.OnEnded -= this.OnEndBooster;
            booster.Stop();
        }
    }

    private void OnEndBooster(Booster booster)
    {
        booster.OnEnded -= this.OnEndBooster;
        this.monoContext.StartCoroutine(this.EndBoosterInNextFrame(booster));
    }

    private IEnumerator EndBoosterInNextFrame(Booster booster)
    {
        yield return new WaitForEndOfFrame();
        this.currentBoosters.Remove(booster);
        this.OnBoosterFinished?.Invoke(booster);
    }
}