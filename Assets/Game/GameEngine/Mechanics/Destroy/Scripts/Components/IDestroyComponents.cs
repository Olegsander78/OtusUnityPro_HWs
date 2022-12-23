using System;


    public interface IComponent_Destroy
    {
        void Destroy(DestroyEvent destroyEvent);
    }

    public interface IComponent_IsDestroyed
    {
        bool IsDestroyed { get; }
    }

    public interface IComponent_OnDestroyed
    {
        public event Action<DestroyEvent> OnDestroyed;
    }