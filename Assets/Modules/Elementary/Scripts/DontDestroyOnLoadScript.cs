using UnityEngine;

namespace Elementary
{
    public sealed class DontDestroyOnLoadScript : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }    
}