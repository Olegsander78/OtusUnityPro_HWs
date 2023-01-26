using System;
using System.Collections;
using System.Collections.Generic;
using GameElements;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ChestsManager : MonoBehaviour,
    IGameStartElement,
    IGameFinishElement
{
    public event Action<Chest> OnChestCountdownStarted;

    public event Action<Chest> OnChestCountdownEnded;

    public event Action<Chest> OnChestActivated;

    [SerializeField]
    private ChestFactory _factory;

    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly List<Chest> _chests = new();

    private readonly List<Chest> _buffer = new();

    [Title("Methods")]
    [Button]
    [GUIColor(0, 1, 0)]
    public void ActivateChest(ChestConfig config)
    {
        var chest = _factory.CreateChest(config);
        chest.OnCompleted += OnEndChestCoundown;

        _chests.Add(chest);

        chest.Start();
        OnChestCountdownStarted?.Invoke(chest);
        OnChestActivated?.Invoke(chest);
    }

    public Chest InstallChest(ChestConfig config)
    {
        var chest = _factory.CreateChest(config);
        chest.OnCompleted += OnEndChestCoundown;

        _chests.Add(chest);
        return chest;
    }

    public Chest[] GetActiveChests()
    {
        return _chests.ToArray();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        StartAllChests();
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        StopAllChests();
    }

    private void StartAllChests()
    {
        _buffer.Clear();
        _buffer.AddRange(_chests);

        for (int i = 0, count = _buffer.Count; i < count; i++)
        {
            var chest = _buffer[i];
            if (chest.IsActive)
            {
                continue;
            }

            chest.Start();
            OnChestCountdownStarted?.Invoke(chest);
        }
    }

    private void StopAllChests()
    {
        _buffer.Clear();
        _buffer.AddRange(_chests);

        for (int i = 0, count = _buffer.Count; i < count; i++)
        {
            var chest = _buffer[i];
            if (!chest.IsActive)
            {
                continue;
            }

            chest.OnCompleted -= OnEndChestCoundown;
            //chest.Stop();
        }
    }

    private void OnEndChestCoundown(Chest chest)
    {
        chest.OnCompleted -= OnEndChestCoundown;
        StartCoroutine(EndChestInNextFrame(chest));
    }

    private IEnumerator EndChestInNextFrame(Chest chest)
    {
        yield return new WaitForEndOfFrame();
        _chests.Remove(chest);
        OnChestCountdownEnded?.Invoke(chest);
        _factory.DisposeChest(chest);
    }
}