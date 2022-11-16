using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;
using System;

public class MoveToPositionMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Vector3 _moveToPositionReceiver;

    [SerializeField]
    private FloatBehaviour _moveSpeed;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnEnable()
    {
        _moveToPositionReceiver.OnEvent += OnMoved;
    }
    private void OnDisable()
    {
        _moveToPositionReceiver.OnEvent -= OnMoved;
    }

    private void OnMoved(Vector3 direction)
    {
        direction.y = 0f;
        _rigidbody.velocity = direction * _moveSpeed.Value;
    }
}
