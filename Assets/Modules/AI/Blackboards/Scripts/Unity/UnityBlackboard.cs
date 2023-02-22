using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Blackboards
{
    [AddComponentMenu("AI/Blackboards/Blackboard")]
    public sealed class UnityBlackboard : MonoBehaviour, IBlackboard
    {
        public event Action<string, object> OnVariableAdded;

        public event Action<string, object> OnVariableChanged;

        public event Action<string, object> OnVariableRemoved;

        private readonly Dictionary<string, object> variables;

        public UnityBlackboard()
        {
            this.variables = new Dictionary<string, object>();
        }

        public IEnumerable<KeyValuePair<string, object>> GetVariables()
        {
            foreach (var variable in this.variables)
            {
                yield return variable;
            }
        }

        public bool TryGetVariable<T>(string key, out T value)
        {
            if (this.variables.TryGetValue(key, out var result))
            {
                value = (T) result;
                return true;
            }

            value = default;
            return false;
        }

        public bool HasVariable(string key)
        {
            return this.variables.ContainsKey(key);
        }

        public T GetVariable<T>(string key)
        {
            if (!this.variables.TryGetValue(key, out var result))
            {
                throw new Exception($"{key} value is not found!");
            }

            return (T) result;
        }

        public void AddVariable(string key, object value)
        {
            if (this.variables.ContainsKey(key))
            {
                throw new Exception($"Variable {key} is already added!");
            }

            this.variables.Add(key, value);
            this.OnVariableAdded?.Invoke(key, value);
        }

        public void ChangeVariable(string key, object value)
        {
            if (!this.variables.ContainsKey(key))
            {
                throw new Exception($"Variable {key} is not found!");
            }

            this.variables[key] = value;
            this.OnVariableChanged?.Invoke(key, value);
        }

        public void RemoveVariable(string key)
        {
            if (this.variables.TryGetValue(key, out var value))
            {
                this.variables.Remove(key);
                this.OnVariableRemoved?.Invoke(key, value);
            }
        }

        public void Clear()
        {
            foreach (var (key, value) in this.variables)
            {
                this.OnVariableRemoved?.Invoke(key, value);
            }
            
            this.variables.Clear();
        }

#if UNITY_EDITOR
        public Dictionary<string, object> Editor_GetVaribles()
        {
            return this.variables;
        }
#endif
    }
}