using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Elementary;

public class EnemyDummy : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Int _takeDamageReceiver;

    public void TakeDamage(int damage)
    {
        _takeDamageReceiver.Call(damage);
    }

}
