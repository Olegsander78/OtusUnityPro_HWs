using UnityEngine;

namespace Elementary
{
    public sealed class TargetFrameRateScript : MonoBehaviour
    {
        [SerializeField]
        private int targetFrameRate = 60;

        private void Awake()
        {
            Application.targetFrameRate = this.targetFrameRate;
        }
    }
}
