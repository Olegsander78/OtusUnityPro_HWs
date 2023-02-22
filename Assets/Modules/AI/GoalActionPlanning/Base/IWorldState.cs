using System;

namespace AI.GOAP
{
    public interface IWorldState
    {
        event Action<Parameter> OnParameterChanged;

        event Action<Parameter> OnParameterAdded;

        event Action<Parameter> OnParameterRemoved; 

        bool GetParameter(string name);

        bool TryGetParameter(string name, out bool value);
        
        Parameter[] GetAllParameters();

        void ChangeParameter(string name, bool value);

        void AddParameter(string name, bool value);

        void RemoveParameter(string name);

        bool ContainsParameter(string name);
    }
}