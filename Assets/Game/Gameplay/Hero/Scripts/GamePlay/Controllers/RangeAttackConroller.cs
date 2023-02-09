using UnityEngine;
using GameSystem;
[AddComponentMenu("Gameplay/Hero/Hero RangeAttack Controller")]
public class RangeAttackConroller : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{    
    private KeyboardInput _input;

    private IComponent_RangeAttack _rangeAttackComponent;

    void IGameInitElement.InitGame()
    {
        
    }

    void IGameStartElement.StartGame()
    {
        _input.OnRangeAttackEvent += RangeAttack;
    }

    void IGameFinishElement.FinishGame()
    {
        _input.OnRangeAttackEvent -= RangeAttack;
    }

    private void RangeAttack()
    {
        _rangeAttackComponent.Attack();
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _input = context.GetService<KeyboardInput>();

        _rangeAttackComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_RangeAttack>();
    }
}
