using System;
using UnityEngine;


[Serializable]
public struct RealtimeData
{
    /// <summary>
    ///     <para>Current time in UTC.</para> 
    /// </summary>
    [SerializeField]
    public long nowSeconds;
}