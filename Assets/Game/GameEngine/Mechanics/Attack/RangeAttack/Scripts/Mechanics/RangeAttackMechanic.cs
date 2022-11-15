using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;
using System;

public class RangeAttackMechanic : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _rangeAttackReciever;

    [SerializeField]
    private FloatBehaviour _attackRate;

    private float _lastAttackTime;

    [SerializeField]
    private FloatBehaviour _speedProjectile;

    private float _lifeTimeProjectile = 3f;

    [SerializeField]
    private GameObject _attackPrefab;


    private void OnEnable()
    {
        _rangeAttackReciever.OnEvent += OnRequestRangeAttack;
    }

    private void OnDisable()
    {
        _rangeAttackReciever.OnEvent -= OnRequestRangeAttack;
    }

    //[GUIColor(0, 1, 0)]
    //[Button]
    private void OnRequestRangeAttack()
    {
        if (Time.time - _lastAttackTime > _attackRate.Value)
        {
            _lastAttackTime = Time.time;
            GameObject proj = Instantiate(_attackPrefab, transform.position + Vector3.up, Quaternion.identity);
            proj.GetComponent<Rigidbody>().velocity = transform.forward * _speedProjectile.Value;
            Destroy(proj, _lifeTimeProjectile);
        }
    }
}
