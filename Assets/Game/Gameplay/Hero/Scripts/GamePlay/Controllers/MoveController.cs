using GameSystem;
using InputModule;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Move Controller")]
public sealed class MoveController : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private JoystickInput input;
    //private KeyboardInput input;

    private IComponent_MoveInDirection heroComponent;

    void IGameInitElement.InitGame()
    {
        
    }

    void IGameStartElement.StartGame()
    {
        this.input.OnDirectionMoved += this.OnDirectionMoved;
    }

    void IGameFinishElement.FinishGame()
    {
        this.input.OnDirectionMoved -= this.OnDirectionMoved;
    }

    private void OnDirectionMoved(Vector2 screenDirection)
    {
        var worldDirection = new Vector3(screenDirection.x, 0.0f, screenDirection.y);
        this.heroComponent.Move(worldDirection);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.input = context.GetService<JoystickInput>();
        //this.input = context.GetService<KeyboardInput>();
        this.heroComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveInDirection>();
    }
}