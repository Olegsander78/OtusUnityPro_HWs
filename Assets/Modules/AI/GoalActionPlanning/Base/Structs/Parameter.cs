using System;
using UnityEngine;

namespace AI.GOAP
{
    [Serializable]
    public struct Parameter
    {
        [SerializeField]
        public string name;

        [SerializeField]
        public bool value;

        public Parameter(string name, bool value)
        {
            this.name = name;
            this.value = value;
        }
    }
}