using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Elementary
{
    [Serializable]
    public sealed class Action_BoolVariable_False : IAction
    {
        private readonly IVariable<bool> variable;

        public Action_BoolVariable_False(IVariable<bool> variable)
        {
            this.variable = variable;
        }

        public void Do()
        {
            this.variable.Value = false;
        }
    }

    [Serializable]
    public sealed class Action_BoolVariable_True : IAction
    {
        private readonly IVariable<bool> variable;

        public Action_BoolVariable_True(IVariable<bool> variable)
        {
            this.variable = variable;
        }

        public void Do()
        {
            this.variable.Value = true;
        }
    }

    [Serializable]
    public sealed class Action_Colliders_Enabled_False : IAction
    {
        [SerializeField]
        public Collider[] colliders;

        public void Do()
        {
            for (int i = 0, count = this.colliders.Length; i < count; i++)
            {
                var collider = this.colliders[i];
                collider.enabled = false;
            }
        }
    }

    [Serializable]
    public sealed class Action_Colliders_Enabled_True : IAction
    {
        [SerializeField]
        public Collider[] colliders;

        public void Do()
        {
            for (int i = 0, count = this.colliders.Length; i < count; i++)
            {
                var collider = this.colliders[i];
                collider.enabled = true;
            }
        }
    }

    [Serializable]
    public sealed class Action_GameObjects_SetActive_False : IAction
    {
        [SerializeField]
        public GameObject[] gameObjects;

        public void Do()
        {
            for (int i = 0, count = this.gameObjects.Length; i < count; i++)
            {
                var gameObject = this.gameObjects[i];
                gameObject.SetActive(false);
            }
        }
    }
    
    [Serializable]
    public sealed class Action_GameObject_SetActive_False : IAction
    {
        [SerializeField]
        public GameObject gameObject;

        public Action_GameObject_SetActive_False(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Action_GameObject_SetActive_False()
        {
        }

        public void Do()
        {
            this.gameObject.SetActive(false);
        }
    }
    
    [Serializable]
    public sealed class Action_GameObject_SetActive_True : IAction
    {
        [SerializeField]
        public GameObject gameObject;

        public void Do()
        {
            this.gameObject.SetActive(true);
        }
    }

    [Serializable]
    public sealed class Action_GameObjects_SetActive_True : IAction
    {
        [SerializeField]
        public GameObject[] gameObjects;

        public void Do()
        {
            for (int i = 0, count = this.gameObjects.Length; i < count; i++)
            {
                var gameObject = this.gameObjects[i];
                gameObject.SetActive(true);
            }
        }
    }

    [Serializable]
    public sealed class Action_AudioSource_PlayOneShot : IAction
    {
        [SerializeField]
        public AudioSource audioSource;

        [SerializeField]
        public AudioClip sound;

        public Action_AudioSource_PlayOneShot()
        {
        }

        public Action_AudioSource_PlayOneShot(AudioSource audioSource, AudioClip sound)
        {
            this.audioSource = audioSource;
            this.sound = sound;
        }

        public void Do()
        {
            this.audioSource.PlayOneShot(this.sound);
        }
    }

    [Serializable]
    public sealed class Action_Animator_Play : IAction
    {
        [Space, SerializeField]
        public Animator animator;

        [Space, SerializeField]
        public string animationName;

        [SerializeField]
        public int layerIndex = -1;

        [SerializeField]
        public float normalizedTime = 0;

        public void Do()
        {
            this.animator.Play(this.animationName, this.layerIndex, this.normalizedTime);
        }
    }

    public static class CommonActionExtensions
    {
        public static IAction SetActiveFalseByAction(this GameObject gameObject)
        {
            return new Action_GameObject_SetActive_False(gameObject);
        }
        
        public static IAction SetTrueByAction(this IVariable<bool> variable)
        {
            return new Action_BoolVariable_True(variable);
        }

        public static IAction SetFalseByAction(this IVariable<bool> variable)
        {
            return new Action_BoolVariable_False(variable);
        }
    }
}