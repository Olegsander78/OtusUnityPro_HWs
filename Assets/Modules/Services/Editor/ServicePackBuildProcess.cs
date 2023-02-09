#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

namespace Services.UnityEditor
{
    public sealed class ServicePackBuildProcess : IPreprocessBuildWithReport
    {
        public int callbackOrder { get; }

        public void OnPreprocessBuild(BuildReport report)
        {
            var guids = AssetDatabase.FindAssets(string.Format("t:{0}", typeof(ServicePack)));
            for (var i = 0; i < guids.Length; i++)
            {
                var id = guids[i];
                var assetPath = AssetDatabase.GUIDToAssetPath(id);
                var servicePack = AssetDatabase.LoadAssetAtPath<ServicePack>(assetPath);
                servicePack.PrepareServicesForBuild();
            }

            AssetDatabase.SaveAssets();
        }
    }
}
#endif