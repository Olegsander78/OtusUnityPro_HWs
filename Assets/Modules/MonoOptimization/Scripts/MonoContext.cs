using System.Collections.Generic;
using UnityEngine;

namespace MonoOptimization
{
    [AddComponentMenu("MonoOptimization/MonoContext")]
    public class MonoContext : MonoBehaviour
    {
        private readonly List<IAwakeComponent> awakeComponents = new();

        private readonly List<IEnableComponent> enableComponents = new();

        private readonly List<IStartComponent> startComponents = new();

        private readonly List<IFixedUpdateComponent> fixedUpdateComponents = new();

        private readonly List<IUpdateComponent> updateComponents = new();

        private readonly List<ILateUpdateComponent> lateUpdateComponents = new();

        private readonly List<IDisableComponent> disableComponents = new();

        private readonly List<IDestroyComponent> destroyComponents = new();

        private readonly List<IValidateComponent> validateComponents = new();

        public void AddMonoComponents(params IMonoComponent[] components)
        {
            for (int i = 0, count = components.Length; i < count; i++)
            {
                var component = components[i];
                this.AddMonoComponent(component);
            }
        }
        
        public void AddMonoComponents(IEnumerable<IMonoComponent> components)
        {
            foreach (var component in components)
            {
                this.AddMonoComponent(component);
            }
        }
        
        public void AddMonoComponent(IMonoComponent component)
        {
            if (component is IAwakeComponent awakeComponent)
            {
                this.awakeComponents.Add(awakeComponent);
            }

            if (component is IEnableComponent enableComponent)
            {
                this.enableComponents.Add(enableComponent);
            }

            if (component is IStartComponent startComponent)
            {
                this.startComponents.Add(startComponent);
            }

            if (component is IFixedUpdateComponent fixedUpdateComponent)
            {
                this.fixedUpdateComponents.Add(fixedUpdateComponent);
            }

            if (component is IUpdateComponent updateComponent)
            {
                this.updateComponents.Add(updateComponent);
            }

            if (component is ILateUpdateComponent lateUpdateComponent)
            {
                this.lateUpdateComponents.Add(lateUpdateComponent);
            }

            if (component is IDisableComponent disableComponent)
            {
                this.disableComponents.Add(disableComponent);
            }
        }

        public void UnregisterMonoComponents(IEnumerable<IMonoComponent> components)
        {
            foreach (var component in components)
            {
                this.UnregisterMonoComponent(component);
            }
        }

        public void UnregisterMonoComponent(IMonoComponent component)
        {
            if (component is IAwakeComponent awakeComponent)
            {
                this.awakeComponents.Remove(awakeComponent);
            }

            if (component is IEnableComponent enableComponent)
            {
                this.enableComponents.Remove(enableComponent);
            }

            if (component is IStartComponent startComponent)
            {
                this.startComponents.Remove(startComponent);
            }

            if (component is IFixedUpdateComponent fixedUpdateComponent)
            {
                this.fixedUpdateComponents.Remove(fixedUpdateComponent);
            }

            if (component is IUpdateComponent updateComponent)
            {
                this.updateComponents.Remove(updateComponent);
            }

            if (component is ILateUpdateComponent lateUpdateComponent)
            {
                this.lateUpdateComponents.Remove(lateUpdateComponent);
            }

            if (component is IDisableComponent disableComponent)
            {
                this.disableComponents.Remove(disableComponent);
            }
        }

        public void Clear()
        {
            this.awakeComponents.Clear();
            this.enableComponents.Clear();
            this.startComponents.Clear();
            this.fixedUpdateComponents.Clear();
            this.updateComponents.Clear();
            this.lateUpdateComponents.Clear();
            this.disableComponents.Clear();
        }

        protected virtual void Awake()
        {
            for (int i = 0, count = this.awakeComponents.Count; i < count; i++)
            {
                var listener = this.awakeComponents[i];
                listener.Awake();
            }
        }

        protected virtual void OnEnable()
        {
            for (int i = 0, count = this.enableComponents.Count; i < count; i++)
            {
                var listener = this.enableComponents[i];
                listener.OnEnable();
            }
        }

        protected virtual void Start()
        {
            for (int i = 0, count = this.startComponents.Count; i < count; i++)
            {
                var listener = this.startComponents[i];
                listener.Start();
            }
        }

        protected virtual void FixedUpdate()
        {
            var deltaTime = Time.fixedDeltaTime;
            for (int i = 0, count = this.fixedUpdateComponents.Count; i < count; i++)
            {
                var listener = this.fixedUpdateComponents[i];
                listener.FixedUpdate(deltaTime);
            }
        }


        protected virtual void Update()
        {
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.updateComponents.Count; i < count; i++)
            {
                var listener = this.updateComponents[i];
                listener.Update(deltaTime);
            }
        }

        protected virtual void LateUpdate()
        {
            var deltaTime = Time.deltaTime;
            for (int i = 0, count = this.lateUpdateComponents.Count; i < count; i++)
            {
                var listener = this.lateUpdateComponents[i];
                listener.LateUpdate(deltaTime);
            }
        }

        protected virtual void OnDisable()
        {
            for (int i = 0, count = this.disableComponents.Count; i < count; i++)
            {
                var listener = this.disableComponents[i];
                listener.OnDisable();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0, count = this.destroyComponents.Count; i < count; i++)
            {
                var listener = this.destroyComponents[i];
                listener.OnDestroy();
            }
        }

        private void OnValidate()
        {
            for (int i = 0, count = this.validateComponents.Count; i < count; i++)
            {
                var listener = this.validateComponents[i];
                listener.OnValidate();
            }
        }
    }
}