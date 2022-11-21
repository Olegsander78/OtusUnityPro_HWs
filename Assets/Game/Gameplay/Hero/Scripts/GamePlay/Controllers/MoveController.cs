using Entities;
using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Move Controller")]
public class MoveController : MonoBehaviour
{
    [SerializeField]
    private KeyboardInput _input;

    [SerializeField]
    private UnityEntityBase _unit;

    private IComponent_MoveInDirection _moveComponent;
    private void Awake()
    {
        _moveComponent = _unit.Get<IComponent_MoveInDirection>();
    }

    private void OnEnable()
    {
        _input.OnMove += Move;
    }

    private void OnDisable()
    {
        _input.OnMove -= Move;
    }

    private void Move(Vector3 direction)
    {
        const float speed = 5.0f;
        Vector3 velocity = direction * (speed * Time.deltaTime);
        _moveComponent.Move(velocity);
    }
}
