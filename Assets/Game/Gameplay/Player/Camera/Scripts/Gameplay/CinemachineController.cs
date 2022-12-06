using Cinemachine;
using UnityEngine;
using GameElements;

[AddComponentMenu("Gameplay/Player/Player Cinemachine Controller")]
public sealed class CinemachineController : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera.enabled = false;
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _virtualCamera.Follow = context.GetService<HeroService>()
            .GetHero()
            .Get<Component_CinemachineFollowPoint>().Point;
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _virtualCamera.enabled = true;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _virtualCamera.enabled = false;
    }
}
