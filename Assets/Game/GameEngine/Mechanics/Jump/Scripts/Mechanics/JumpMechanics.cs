using UnityEngine;
using Elementary;

public class JumpMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _jumpReceiver;

    [SerializeField]
    private FloatBehaviour _forceJump;

    [SerializeField]
    private GroundChecker _groundChecker;

    [SerializeField]
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _jumpReceiver.OnEvent += OnTryJumped;
    }

    private void OnDisable()
    {
        _jumpReceiver.OnEvent -= OnTryJumped;
    }

    public void OnTryJumped()
    {    
        if (_groundChecker.IsGrounded())
            _rigidbody.AddForce(Vector3.up * _forceJump.Value, ForceMode.Impulse);
    }
}
