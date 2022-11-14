using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;
using System;

public class DeathMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _deathReceiver;

    [SerializeField]
    private IntBehaviour _hitPoints;

    private void OnEnable()
    {
        _hitPoints.OnValueChanged += OnHitPointsChanged;
    }    

    private void OnDisable()
    {
        _hitPoints.OnValueChanged -= OnHitPointsChanged;
    }
    private void OnHitPointsChanged(int newHitPoints)
    {
        if(newHitPoints <= 0)
        {
            _deathReceiver.Call();
        }
    }
}
