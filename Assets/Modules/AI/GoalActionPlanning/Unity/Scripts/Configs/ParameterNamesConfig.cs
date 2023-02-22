#if UNITY_EDITOR
using UnityEngine;

namespace AI.GOAP.UnityEditor
{
    [CreateAssetMenu(
        fileName = "ParameterNamesConfig",
        menuName = "AI/GOAP/ParameterNamesConfig"
    )]
    public sealed class ParameterNamesConfig : ScriptableObject
    {
        [SerializeField]
        private string[] names;

        private static ParameterNamesConfig instance;

        public static string[] GetNames()
        {
            if (instance == null)
            {
                instance = Resources.Load<ParameterNamesConfig>("ParameterNamesConfig");
            }

            return instance.names;
        }
    }
}
#endif