using UnityEngine;


public sealed class UComponent_BoosterInfo : MonoBehaviour, IComponent_BoosterInfo
{
    public BoosterConfig BoosterInfo
    {
        get { return this.config; }
    }

    [SerializeField]
    private BoosterConfig config;
}