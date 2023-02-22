using System;
using System.Collections.Generic;

namespace AI.Blackboards
{
    public interface IBlackboard
    {
        event Action<string, object> OnVariableAdded; 

        event Action<string, object> OnVariableChanged;

        event Action<string, object> OnVariableRemoved; 
        
        T GetVariable<T>(string key);

        IEnumerable<KeyValuePair<string, object>> GetVariables();

        bool TryGetVariable<T>(string key, out T value);
        
        bool HasVariable(string key);

        void AddVariable(string key, object value);

        void ChangeVariable(string key, object value);

        void RemoveVariable(string key);

        void Clear();
    }
}