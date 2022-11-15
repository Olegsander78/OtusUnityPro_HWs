using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;
using System;

public class MoveMechanics : MonoBehaviour
{   
    [SerializeField]
    private FloatBehaviour _moveSpeed;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    private void ForwardMove()
    {
        _rigidbody.velocity = transform.forward * _moveSpeed.Value;
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void BackMove()
    {
        _rigidbody.velocity = transform.forward * -_moveSpeed.Value;
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void LeftMove()
    {
        _rigidbody.velocity = transform.right * -_moveSpeed.Value;
    }

    [GUIColor(0, 1, 0)]
    [Button]
    private void RightMove()
    {
        _rigidbody.velocity = transform.right * _moveSpeed.Value;
    }

}
