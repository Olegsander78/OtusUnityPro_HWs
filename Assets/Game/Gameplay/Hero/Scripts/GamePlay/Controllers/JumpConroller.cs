using UnityEngine;
using GameSystem;

[AddComponentMenu("Gameplay/Hero/Hero Jump Controller")]
public class JumpConroller : MonoBehaviour,
    IGameConstructElement,
    IGameStartElement,
    IGameFinishElement
{
    private KeyboardInput _input;

    private IComponent_Jump _jumpComponent;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _jumpComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Jump>();
    }


    void IGameStartElement.StartGame()
    {
        _input.OnJumpEvent += Jump;
    }

    void IGameFinishElement.FinishGame()
    {
        _input.OnJumpEvent -= Jump;
    }

    protected void Jump()
    {
        _jumpComponent.Jump();
    }
}
