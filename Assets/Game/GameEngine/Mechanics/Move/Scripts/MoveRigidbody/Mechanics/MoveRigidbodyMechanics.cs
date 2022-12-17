using UnityEngine;
using Elementary;
using Entities;

public class MoveRigidbodyMechanics : MonoBehaviour
{
    [SerializeField]
    private UnityEntity _player;
    
    [SerializeField]
    private EventReceiver_Vector3 _moveInDirectionReceiver;

    [SerializeField]
    private FloatBehaviour _moveSpeed;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private Vector3 _transform;

    private void Awake()
    {
        //_rigidbody = GetComponentInParent<Rigidbody>();
        //_transform = GetComponentInParent<Transform>();
        _transform = _player.Get<IComponent_GetPosition>().Position;
        _rigidbody = _player.GetComponent<Rigidbody>();
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
        _transform = _player.GetComponent<IComponent_GetPosition>().Position;

        //Vector3 dir = (_transform.right * direction.x) + (_transformEngine.transform.forward * direction.z);
        _transform.Normalize();        

        _rigidbody.velocity = _transform * _moveSpeed.Value;               
    }
}
