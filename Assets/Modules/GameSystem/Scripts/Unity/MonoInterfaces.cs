namespace GameSystem
{
    public interface IGameUpdateElement
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateElement
    {
        void OnFixedUpdate(float deltaTime);
    }

    public interface IGameLateUpdateElement
    {
        void OnLateUpdate(float deltaTime);
    }
}