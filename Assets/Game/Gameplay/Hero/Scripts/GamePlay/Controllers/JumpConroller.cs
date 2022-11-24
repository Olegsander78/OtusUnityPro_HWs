using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Jump Controller")]
public class JumpConroller : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    private KeyboardInput _input;

    private IComponent_Jump _jumpComponent;

    public void Construct(GameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _jumpComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Jump>();
    }

    void IStartGameListener.OnStartGame()
    {
        _input.OnJumpEvent += Jump;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _input.OnJumpEvent -= Jump;
    }

    protected void Jump()
    {
        _jumpComponent.Jump();
    }
}
