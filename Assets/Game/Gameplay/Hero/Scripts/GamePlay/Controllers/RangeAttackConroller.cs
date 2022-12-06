using UnityEngine;
using GameElements;

[AddComponentMenu("Gameplay/Hero/Hero RangeAttack Controller")]
public class RangeAttackConroller : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{    
    private KeyboardInput _input;

    private IComponent_RangeAttack _rangeAttackComponent;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _rangeAttackComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_RangeAttack>();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _input.OnRangeAttackEvent += RangeAttack;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _input.OnRangeAttackEvent -= RangeAttack;
    }

    private void RangeAttack()
    {
        _rangeAttackComponent.Attack();
    }
}
