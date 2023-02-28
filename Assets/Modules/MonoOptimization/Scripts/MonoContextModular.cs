using System;
using UnityEngine;

namespace MonoOptimization
{
    [AddComponentMenu("MonoOptimization/Mono Context «Modular»")]
    public class MonoContextModular : MonoContext
    {
        [Space]
        [SerializeField]
        private MonoModule[] modules = new MonoModule[0];

        protected override void Awake()
        {
            this.RegisterComponents();
            this.Construct();
            base.Awake();
        }

        public T GetModule<T>() where T : MonoModule
        {
            for (int i = 0, count = this.modules.Length; i < count; i++)
            {
                var module = this.modules[i];
                if (module is T result)
                {
                    return result;
                }
            }

            throw new Exception($"Module of type {typeof(T).Name} is not found!");
        }

        public bool TryGetModule(Type type, out object module)
        {
            for (int i = 0, count = this.modules.Length; i < count; i++)
            {
                var currentModule = this.modules[i];
                var currentType = currentModule.GetType();

                if (type.IsAssignableFrom(currentType))
                {
                    module = currentModule;
                    return true;
                }
            }

            module = default;
            return false;
        }

        private void RegisterComponents()
        {
            var count = this.modules.Length;
            for (var i = 0; i < count; i++)
            {
                var module = this.modules[i];
                if (module != null)
                {
                    var registeredObjects = module.ProvideMonoComponents();
                    this.AddMonoComponents(registeredObjects);
                }
            }
        }

        [ContextMenu("Construct")]
        private void Construct()
        {
            var count = this.modules.Length;
            for (var i = 0; i < count; i++)
            {
                var module = this.modules[i];
                module.ConstructSensor(this);
            }
        }
    }
}