using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;

public class JumpMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Float _jumpReceiver;

    [SerializeField]
    private FloatBehaviour _forceJump;

    [SerializeField] 
    private float _groundPointRadius;

    [SerializeField]
    private Transform _groundPoint;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
    }

    private void OnEnable()
    {
        _jumpReceiver.OnEvent += OnTryJumped;
    }

    private void OnDisable()
    {
        _jumpReceiver.OnEvent -= OnTryJumped;
    }

    public void OnTryJumped(float jumpForce)
    {
        jumpForce = _forceJump.Value;

        if (Grounded())
            Jump(jumpForce);
    }
    private void Jump(float force)
    {        
        _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    private bool Grounded()
    {
        return Physics.CheckSphere(_groundPoint.position, _groundPointRadius, _whatIsGround);
    }
}
