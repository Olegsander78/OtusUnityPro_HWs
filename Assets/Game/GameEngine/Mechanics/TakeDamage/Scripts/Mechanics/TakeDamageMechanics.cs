using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;

public class TakeDamageMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _takeDamageReceiver;

    [SerializeField]
    private IntBehaviour _hitPoints;

    private void OnEnable()
    {
        _takeDamageReceiver.OnEvent += OnDamageTaken;
    }

    private void OnDisable()
    {
        _takeDamageReceiver.OnEvent -= OnDamageTaken;
    }

    //private void OnDamageTaken(int damage)
    //{
    //    this.hitPoints.Value -= damage;
    //}

    private void OnDamageTaken()
    {
        const int damage = 1;
        _hitPoints.Value -= damage;
    }

}
