using UnityEngine;
using Entities;
using Sirenix.OdinInspector;

[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
public class DieController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    private GameContext _context;

    private IComponent_Die _dieComponent;

    private IComponent_MoveOnPosition _moveOnStartPositionComponent;

    [SerializeField]
    private Transform _respawnPoint;

    public void Construct(GameContext context)
    {
        _context = context;

        _dieComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_Die>();

        _moveOnStartPositionComponent = context.GetService<HeroService>()
            .GetHero()
            .Get<IComponent_MoveOnPosition>();
        
    }
    void IStartGameListener.OnStartGame()
    {
        _dieComponent.OnDieEvent += OnHeroDestroyed;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _dieComponent.OnDieEvent -= OnHeroDestroyed;
    }

    [Button]
    private void OnHeroDestroyed()
    {
        _moveOnStartPositionComponent.Move(_respawnPoint.position);
        Invoke(nameof(FinishGameWithDelay), 1f);
    } 
    
    private void FinishGameWithDelay()
    {
        _context.FinishGame();
    }
}

