using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;
using System;

public class MoveInDirectionMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Vector3 _moveInDirectionReceiver;

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
        _moveInDirectionReceiver.OnEvent += OnMoved;
    }
    private void OnDisable()
    {
        _moveInDirectionReceiver.OnEvent -= OnMoved;
    }

    private void OnMoved(Vector3 direction)
    {        
        Vector3 dir = (transform.right * direction.x) + (transform.forward * direction.z);
        dir.Normalize();
        _rigidbody.velocity = dir * _moveSpeed.Value;
    }
}
