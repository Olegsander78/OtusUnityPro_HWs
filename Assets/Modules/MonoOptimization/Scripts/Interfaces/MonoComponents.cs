namespace MonoOptimization
{
    public interface IMonoComponent
    {
    }

    public interface IAwakeComponent : IMonoComponent
    {
        void Awake();
    }

    public interface IEnableComponent : IMonoComponent
    {
        void OnEnable();
    }

    public interface IStartComponent : IMonoComponent
    {
        void Start();
    }

    public interface IFixedUpdateComponent : IMonoComponent
    {
        void FixedUpdate(float deltaTime);
    }

    public interface IUpdateComponent : IMonoComponent
    {
        void Update(float deltaTime);
    }

    public interface ILateUpdateComponent : IMonoComponent
    {
        void LateUpdate(float deltaTime);
    }

    public interface IDisableComponent : IMonoComponent
    {
        void OnDisable();
    }

    public interface IDestroyComponent : IMonoComponent
    {
        void OnDestroy();
    }

    public interface IValidateComponent : IMonoComponent
    {
        void OnValidate();
    }
}