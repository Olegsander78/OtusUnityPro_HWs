using UnityEngine;
using Entities;
using Sirenix.OdinInspector;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    private GameContext _gameContext;

    private IComponent_Die _dieComponent;

    private IComponent_MoveOnPosition _moveOnStartPositionComponent;

    [SerializeField]
    private Transform _respawnPoint;

    public void Construct(GameContext context)
    {
        _gameContext = context;

        _dieComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Die>();

        _moveOnStartPositionComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveOnPosition>();
        
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
        _moveOnStartPositionComponent.Move(_respawnPoint.position);
        Invoke(nameof(FinishGameWithDelay), 1f);
    } 
    
    private void FinishGameWithDelay()
    {
        _gameContext.FinishGame();
    }
}

