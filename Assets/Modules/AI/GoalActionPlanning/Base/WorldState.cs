using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI.GOAP
{
    [Serializable]
    public sealed class WorldState : IWorldState
    {
        public event Action<Parameter> OnParameterChanged;
        
        public event Action<Parameter> OnParameterAdded;
        
        public event Action<Parameter> OnParameterRemoved;

        [SerializeField]
        private List<Parameter> fields;

        public void ChangeParameter(string name, bool value)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    field.value = value;
                    this.fields[i] = field;
                    this.OnParameterChanged?.Invoke(new Parameter(name, value));
                    return;
                }
            }
            
            throw new Exception($"Value {name} is not found");
        }

        public void AddParameter(string name, bool value)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    throw new Exception($"Value {name} is already added!");
                }
            }

            var newField = new Parameter(name, value);
            this.fields.Add(newField);
            this.OnParameterAdded?.Invoke(newField);
        }

        public void RemoveParameter(string name)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    this.fields.RemoveAt(i);
                    this.OnParameterRemoved?.Invoke(field);
                    return;
                }
            }
        }

        public bool GetParameter(string name)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    return field.value;
                }
            }

            throw new Exception($"Field {name} is not found");
        }
        
        public Parameter[] GetAllParameters()
        {
            return this.fields.ToArray();
        }

        public bool ContainsParameter(string name)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryGetParameter(string name, out bool value)
        {
            for (int i = 0, count = this.fields.Count; i < count; i++)
            {
                var field = this.fields[i];
                if (field.name == name)
                {
                    value = field.value;
                    return true;
                }
            }

            value = default;
            return false;
        }
    }
}