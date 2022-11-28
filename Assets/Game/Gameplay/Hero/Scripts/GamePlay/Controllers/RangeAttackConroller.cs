using UnityEngine;
using Entities;

[AddComponentMenu("Gameplay/Hero/Hero RangeAttack Controller")]
public class RangeAttackConroller : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{    
    private KeyboardInput _input;

    private IComponent_RangeAttack _rangeAttackComponent;

    void IConstructListener.Construct(GameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _rangeAttackComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_RangeAttack>();
    }

    void IStartGameListener.OnStartGame()
    {
        _input.OnRangeAttackEvent += RangeAttack;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _input.OnRangeAttackEvent -= RangeAttack;
    }

    private void RangeAttack()
    {
        _rangeAttackComponent.Attack();
    }
}
