using Entities;
using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Move Controller")]
public class MoveController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    
    private KeyboardInput _input;

    private IComponent_MoveInDirection _moveComponent;

    //[SerializeField]
    //private UnityEntityBase _unit;

    //private IComponent_MoveInDirection _moveComponent;
    //private void Awake()
    //{
    //    _moveComponent = _unit.Get<IComponent_MoveInDirection>();
    //}

    void IConstructListener.Construct(GameContext context)
    {
        _input = context.GetService<KeyboardInput>();
        _moveComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveInDirection>();
    }

    void IStartGameListener.OnStartGame()
    {
        _input.OnMove += Move;
    }

    void IFinishGameListener.OnFinishGame()
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
