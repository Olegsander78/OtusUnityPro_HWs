using System;
using UnityEngine;

[AddComponentMenu("Gameplay/Hero/Hero Jump Controller")]
public class JumpConroller : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    public event Action OnJumpEvent;

    private IComponent_Jump _jumpComponent;

    private void Update()
    {
        HandleKeyboard();
    }
    public void Construct(GameContext context)
    {
        _jumpComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Jump>();
    }

    void IStartGameListener.OnStartGame()
    {
        OnJumpEvent += Jump;
    }

    void IFinishGameListener.OnFinishGame()
    {
        OnJumpEvent -= Jump;
    }
    
    private void HandleKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnJump();
        }
    }

    private void OnJump()
    {
        OnJumpEvent?.Invoke();
    }

    protected void Jump()
    {
        _jumpComponent.Jump();
    }
}
