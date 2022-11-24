using Cinemachine;
using UnityEngine;


[AddComponentMenu("Gameplay/Player/Player Cinemachine Controller")]
public sealed class CinemachineController : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera.enabled = false;
    }

    void IConstructListener.Construct(GameContext context)
    {
        _virtualCamera.Follow = context.GetService<HeroService>()
            .GetHero()
            .Get<Component_CinemachineFollowPoint>().Point;
    }

    void IStartGameListener.OnStartGame() 
    {
        _virtualCamera.enabled = true;
    }

    void IFinishGameListener.OnFinishGame()
    {
        _virtualCamera.enabled = false;
    }
}
