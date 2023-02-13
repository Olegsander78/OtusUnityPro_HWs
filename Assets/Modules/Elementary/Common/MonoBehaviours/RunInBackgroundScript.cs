using UnityEngine;

namespace Elementary
{
    public sealed class RunInBackgroundScript : MonoBehaviour
    {
        [SerializeField]
        private bool runInBackground;

        private void Awake()
        {
            Application.runInBackground = this.runInBackground;
        }
    }
}