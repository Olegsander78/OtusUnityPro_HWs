using UnityEngine;
using GameElements;
public sealed class GameContextInstaller : MonoBehaviour
{
    [SerializeField]
    private IGameContext _context;

    [Space]
    [SerializeField]
    private MonoBehaviour[] _elements;

    [Space]
    [SerializeField]
    private MonoBehaviour[] _services;

    private void Awake()
    {
        foreach (object service in _services)
        {
            _context.RegisterService(service);
        }

        foreach (var element in _elements)
        {
            _context.RegisterElement((IGameElement)element);
        }
    }
}
