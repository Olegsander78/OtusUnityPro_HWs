using System;

namespace Elementary
{
    public interface IEmitter
    {
        event Action OnEvent;

        void AddListener(IAction listener);

        void RemoveListener(IAction listener);

        void Call();
    }

    public interface IEmitter<T>
    {
        event Action<T> OnEvent;
        
        void AddListener(IAction listener);

        void RemoveListener(IAction listener);

        void AddListener(IAction<T> listener);

        void RemoveListener(IAction<T> listener);

        void Call(T args);
    }
}