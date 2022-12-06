using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using GameElements;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private const float DELAY = 1f;
    
    private IGameContext _gameContext;

    private IComponent_Die _dieComponent;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _gameContext = context;

        _dieComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Die>();
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _dieComponent.OnDestroyedEvent += OnHeroDestroyed;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
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