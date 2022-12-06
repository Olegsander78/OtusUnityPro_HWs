using Entities;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Service")]
public sealed class HeroService : MonoBehaviour
{
    [SerializeField]
    private UnityEntity _currentHero;

    public void SetupHero(UnityEntity hero)
    {
        _currentHero = hero;
    }

    public IEntity GetHero()
    {       
        return _currentHero;
    }
}
