#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Game.Development
{
    [CustomEditor(typeof(TileSnap))]
    public sealed class TileSnapEditor : Editor
    {
        private const int TILE_SIZE = 6;

        private TileSnap targetObject;

        private void OnEnable()
        {
            this.targetObject = (TileSnap) this.target;
        }

        private void OnSceneGUI()
        {
            var e = Event.current;
            if (e.type == EventType.MouseUp)
            {
                var targetTransform = this.targetObject.transform;
                TileSnapper.Snap(targetTransform, TILE_SIZE);
            }
        }
    }
}
#endif