using System;
using System.Collections.Generic;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class ConditionComposite : ICondition
    {
        [SerializeField]
        private Mode mode;
        
        [SerializeField]
        private List<ICondition> conditions;
        
        public ConditionComposite()
        {
            this.conditions = new List<ICondition>();
            this.mode = Mode.AND;
        }

        public ConditionComposite(Mode mode, params ICondition[] conditions)
        {
            this.conditions = new List<ICondition>(conditions);
            this.mode = mode;
        }

        public ConditionComposite SetMode(Mode mode)
        {
            this.mode = mode;
            return this;
        }

        public ConditionComposite AddCondition(ICondition condition)
        {
            this.conditions.Add(condition);
            return this;
        }

        public bool IsTrue()
        {
            return this.mode switch
            {
                Mode.AND => this.All(),
                Mode.OR => this.Any(),
                _ => throw new Exception($"Mode is undefined {this.mode}")
            };
        }

        private bool All()
        {
            for (int i = 0, count = this.conditions.Count; i < count; i++)
            {
                var condition = this.conditions[i];
                if (!condition.IsTrue())
                {
                    return false;
                }
            }

            return true;
        }

        private bool Any()
        {
            for (int i = 0, count = this.conditions.Count; i < count; i++)
            {
                var condition = this.conditions[i];
                if (condition.IsTrue())
                {
                    return true;
                }
            }

            return false;
        }

        public enum Mode
        {
            AND,
            OR
        }
    }
}