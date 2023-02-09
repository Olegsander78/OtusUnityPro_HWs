using UnityEngine;
using GameSystem;

[AddComponentMenu("Gameplay/Hero/Hero Move Physics Controller")]
public class MovePhysicsController : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{    
    private KeyboardInput _input;

    private IComponent_MoveRigidbody _moveComponent;

    void IGameInitElement.InitGame()
    {
        
    }

    void IGameStartElement.StartGame()
    {
        _input.OnMoveEvent += Move;
    }

    void IGameFinishElement.FinishGame()
    {
        _input.OnMoveEvent -= Move;
    }
    private void Move(Vector3 direction)
    {         
        Vector3 velocity = direction * Time.deltaTime;
        _moveComponent.Move(velocity);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _input = context.GetService<KeyboardInput>();
        _moveComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveRigidbody>();
    }
}
