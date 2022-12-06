using UnityEngine;
using GameElements;

[AddComponentMenu("Gameplay/Hero/Hero Jump Controller")]
public class JumpConroller : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private KeyboardInput _input;

    private IComponent_Jump _jumpComponent;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _jumpComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Jump>();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _input.OnJumpEvent += Jump;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _input.OnJumpEvent -= Jump;
    }

    protected void Jump()
    {
        _jumpComponent.Jump();
    }
}
