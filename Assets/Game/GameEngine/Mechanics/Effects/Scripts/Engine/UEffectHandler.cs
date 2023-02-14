using UnityEngine;


public abstract class UEffectHandler : MonoBehaviour
{
    public abstract void OnEffectAdded(IEffect effect);

    public abstract void OnEffectRemoved(IEffect effect);
}