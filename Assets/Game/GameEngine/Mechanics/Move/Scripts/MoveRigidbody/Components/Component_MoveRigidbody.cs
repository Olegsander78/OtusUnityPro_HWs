//NO BUSSINESS LOGIC
using UnityEngine;
using Elementary;
using System;

public class Component_MoveRigidbody : MonoBehaviour, IComponent_MoveRigidbody
{
    [SerializeField]
    private EventReceiver_Vector3 _moveReceiver;

    public event Action<float> OnSpeedChanged
    {
        add { _speed.OnValueChanged += value; }
        remove { _speed.OnValueChanged -= value; }
    }

    public float Speed
    {
        get { return _speed.Value; }
    }

    [SerializeField]
    private FloatBehaviour _speed;

    public void Move(Vector3 direction)
    {
        _moveReceiver.Call(direction);
    }

    public void SetSpeed(float speed)
    {
        _speed.Value = speed;
    }
}
