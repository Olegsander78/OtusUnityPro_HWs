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

    private IEntity _unit;

    void IConstructListener.Construct(GameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _moveComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveInDirection>();

        _unit = context.GetService<HeroService>().GetHero();
    }

    void IStartGameListener.OnStartGame()
    {
        _input.OnMoveEvent += Move;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _input.OnMoveEvent -= Move;
    }
    private void Move(Vector3 direction)
    {
        //direction.y = _unit.Get<Transform>().position.y;        
        Vector3 velocity = direction * Time.deltaTime;
        _moveComponent.Move(velocity);
    }
}
