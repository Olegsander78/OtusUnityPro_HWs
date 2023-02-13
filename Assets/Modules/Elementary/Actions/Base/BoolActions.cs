using System;
using UnityEngine;

namespace Elementary
{
    [Serializable]
    public sealed class BoolAction_Colliders_Enabled : IAction<bool>
    {
        [SerializeField]
        public Collider[] colliders;

        public void Do(bool enabled)
        {
            for (int i = 0, count = this.colliders.Length; i < count; i++)
            {
                var collider = this.colliders[i];
                collider.enabled = enabled;
            }
        }
    }
    
    [Serializable]
    public sealed class BoolAction_Collider_Enabled : IAction<bool>
    {
        [SerializeField]
        public Collider collider;

        public void Do(bool enabled)
        {
            this.collider.enabled = enabled;
        }
    }

    [Serializable]
    public sealed class BoolAction_GameObjects_SetActive : IAction<bool>
    {
        [SerializeField]
        public GameObject[] gameObjects;

        public void Do(bool isActive)
        {
            for (int i = 0, count = this.gameObjects.Length; i < count; i++)
            {
                var gameObject = this.gameObjects[i];
                gameObject.SetActive(isActive);
            }
        }
    }
    
    
    [Serializable]
    public sealed class BoolAction_GameObject_SetActive : IAction<bool>
    {
        [SerializeField]
        public GameObject gameObject;

        public void Do(bool isActive)
        {
            this.gameObject.SetActive(isActive);
        }
    }

    public static class BoolActionExtensions
    {
        public static IAction<bool> EnabledByAction(this Collider[] colliders)
        {
            return new BoolAction_Colliders_Enabled
            {
                colliders = colliders
            };
        }
        
        public static IAction<bool> EnabledByAction(this Collider collider)
        {
            return new BoolAction_Collider_Enabled
            {
                collider = collider
            };
        }
        
        public static IAction<bool> SetActiveByAction(this GameObject gameObject)
        {
            return new BoolAction_GameObject_SetActive
            {
                gameObject = gameObject
            };
        }

        public static IAction<bool> SetActiveByAction(this GameObject[] gameObjects)
        {
            return new BoolAction_GameObjects_SetActive
            {
                gameObjects = gameObjects
            };
        }
    }
}