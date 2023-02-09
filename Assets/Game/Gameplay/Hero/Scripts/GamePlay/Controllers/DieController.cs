using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using GameSystem;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private const float DELAY = 1f;
    
    private IGameContext _gameContext;

    private IComponent_Die _dieComponent;

    public virtual void ConstructGame(IGameContext context)
    {
        _gameContext = context;
    }

    void IGameInitElement.InitGame()
    {        
        _dieComponent = _gameContext.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Die>();
    }

    void IGameStartElement.StartGame()
    {
        _dieComponent.OnDestroyedEvent += OnHeroDestroyed;
    }

    void IGameFinishElement.FinishGame()
    {
        _dieComponent.OnDestroyedEvent -= OnHeroDestroyed;
    }

    [Button]
    private void OnHeroDestroyed()
    {
        StartCoroutine(FinishGameWithDelayRoutina());
    } 
    
    private IEnumerator FinishGameWithDelayRoutina()
    {
        yield return new WaitForSeconds(DELAY);
        _gameContext.FinishGame();
    }
}