using UnityEngine;
using Elementary;

public class MoveInDirectionMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Vector3 _moveInDirectionReceiver;

    [SerializeField]
    private FloatBehaviour _moveSpeed;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Transform _transform;

    private void Awake()
    {
        _rigidbody = GetComponentInParent<Rigidbody>();
        _transform = GetComponentInParent<Transform>();
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
        Vector3 dir = (_transform.right * direction.x) + (_transform.forward * direction.z);
        dir.Normalize();
        _rigidbody.velocity = dir * _moveSpeed.Value;
    }
}
