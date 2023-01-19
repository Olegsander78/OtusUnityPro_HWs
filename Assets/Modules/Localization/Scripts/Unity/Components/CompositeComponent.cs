using System;
using UnityEngine;

namespace LocalizationModule
{
    [Serializable]
    public sealed class CompositeComponent : IComponent
    {
        [SerializeField]
        private IComponent[] children = new IComponent[0];

        public CompositeComponent()
        {
        }

        public CompositeComponent(params IComponent[] children)
        {
            this.children = children;
        }

        public void UpdateLanguage(SystemLanguage language)
        {
            for (int i = 0, count = this.children.Length; i < count; i++)
            {
                var child = this.children[i];
                child.UpdateLanguage(language);
            }
        }
    }
}