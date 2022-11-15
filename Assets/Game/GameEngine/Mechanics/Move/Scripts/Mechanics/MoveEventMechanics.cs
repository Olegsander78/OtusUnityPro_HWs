using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Elementary;

public class MoveEventMechanics : MonoBehaviour
{
    [Header("Move Forward")]
    [SerializeField]
    private EventReceiver_Float _moveForwardReceiver;
    [Header("Move Back")]
    [SerializeField]
    private EventReceiver_Float _moveBackReceiver;
    [Header("Move Left")]
    [SerializeField]
    private EventReceiver_Float _moveLeftReceiver;
    [Header("Move Right")]
    [SerializeField]
    private EventReceiver_Float _moveRightReceiver;

    [Space(10)]
    [SerializeField]
    private FloatBehaviour _moveSpeed;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnEnable()
    {
        _moveForwardReceiver.OnEvent += OnForwardMoved;
        _moveBackReceiver.OnEvent += OnBackMoved;
        _moveLeftReceiver.OnEvent += OnLeftMoved;
        _moveRightReceiver.OnEvent += OnRightMoved;
    }

    private void OnDisable()
    {
        _moveForwardReceiver.OnEvent -= OnForwardMoved;
        _moveBackReceiver.OnEvent -= OnBackMoved;
        _moveLeftReceiver.OnEvent -= OnLeftMoved;
        _moveRightReceiver.OnEvent -= OnRightMoved;
    }
    
    private void OnForwardMoved(float speed)
    {
        //speed = _moveSpeed.Value;
        _rigidbody.velocity = transform.forward * speed;
    }
    private void OnBackMoved(float speed)
    {
        //speed = _moveSpeed.Value;
        _rigidbody.velocity = transform.forward * -speed;
    }
    private void OnLeftMoved(float speed)
    {
        //speed = _moveSpeed.Value;
        _rigidbody.velocity = transform.right * -speed;
    }
    private void OnRightMoved(float speed)
    {
        //speed = _moveSpeed.Value;
        _rigidbody.velocity = transform.right * speed;
    }

}
