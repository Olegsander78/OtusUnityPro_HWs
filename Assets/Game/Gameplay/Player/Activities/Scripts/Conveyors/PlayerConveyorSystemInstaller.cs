using GameSystem;
using Sirenix.OdinInspector;
using static GameSystem.GameComponentType;


public sealed class PlayerConveyorSystemInstaller : MonoGameInstaller
{
    [GameComponent(SERVICE)]
    [ReadOnly, ShowInInspector]
    private VisitingConveyorService service = new();

    [GameComponent(ELEMENT)]
    [ReadOnly, ShowInInspector]
    private VisitingConveyorObserver_AddResourcesToPlayer addResourcesObserver = new();

    [GameComponent(ELEMENT)]
    [ReadOnly, ShowInInspector]
    private VisitingConveyorObserver_ExtractPlayerResources extractResourcesObserver = new();
}