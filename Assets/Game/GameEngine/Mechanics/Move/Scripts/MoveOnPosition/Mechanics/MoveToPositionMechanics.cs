using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Elementary;
using System;

public class MoveToPositionMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Vector3 _moveToPositionReceiver;       

    [SerializeField]
    private Transform _transform;

    private void OnEnable()
    {
        _moveToPositionReceiver.OnEvent += OnMoved;
    }
    private void OnDisable()
    {
        _moveToPositionReceiver.OnEvent -= OnMoved;
    }

    private void OnMoved(Vector3 position)
    {
        _transform.position = position;
    }
}
