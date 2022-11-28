using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    private const float DELAY = 1f;
    
    private GameContext _gameContext;

    private IComponent_Die _dieComponent;

    public void Construct(GameContext context)
    {
        _gameContext = context;

        _dieComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Die>();
    }
        
    void IStartGameListener.OnStartGame()
    {
        _dieComponent.OnDestroyedEvent += OnHeroDestroyed;
    }

    void IFinishGameListener.OnFinishGame()
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