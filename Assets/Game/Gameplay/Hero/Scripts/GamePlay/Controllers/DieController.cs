//using UnityEngine;
//using Entities;

//[AddComponentMenu("Gameplay/Hero/Hero Die Controller")]
//public class DieController : MonoBehaviour, 
//    IStartGameListener,
//    IFinishGameListener
//{
//    private UnityEntity _unit;

//    private IComponent_Die _dieComponent;

//    private IComponent_TakeDamage _takeDamageComponent;

//    private void Awake()
//    {
//        _dieComponent = _unit.Get<IComponent_Die>();
//        _takeDamageComponent = _unit.Get<IComponent_TakeDamage>();
//    }

//    void IStartGameListener.OnStartGame()
//    {
//        _takeDamageComponent.TakeDamage += OnHeroDestroyed;
//    }

//    void IFinishGameListener.OnFinishGame()
//    {
//        _takeDamageComponent.TakeDamage -= OnHeroDestroyed;
//    }

//    private void OnHeroDestroyed()
//    {
        
//        _dieComponent.Die();
//    }
//}

