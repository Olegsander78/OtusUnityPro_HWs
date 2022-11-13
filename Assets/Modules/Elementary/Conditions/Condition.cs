using System;
using UnityEngine;


namespace Elementary
{
    [Serializable]
    public sealed class Condition : ICondition
    {
        [SerializeField]
        private bool isTrue;

        public Condition()
        {
        }

        public Condition(bool isTrue)
        {
            this.isTrue = isTrue;
        }

        public void SetTrue(bool isTrue)
        {
            this.isTrue = isTrue;
        }

        public bool IsTrue()
        {
            return this.isTrue;
        }
    }
}
