using Cinemachine;
using UnityEngine;
using GameSystem;

[AddComponentMenu("Gameplay/Player/Player Cinemachine Controller")]
public sealed class CinemachineController : MonoBehaviour,
    IGameConstructElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera;

    private void Awake()
    {
        _virtualCamera.enabled = false;
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _virtualCamera.Follow = context.GetService<HeroService>()
            .GetHero()
            .Get<Component_CinemachineFollowPoint>().Point;
    }

    void IGameStartElement.StartGame()
    {
        _virtualCamera.enabled = true;
    }

    void IGameFinishElement.FinishGame()
    {
        _virtualCamera.enabled = false;
    }
}
