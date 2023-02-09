using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/Prepare Pipeline")]
public sealed class PreparePipeline : MonoBehaviour
{
    [Space]
    [SerializeField]
    private MonoGameContext gameContext;

    [Space]
    [ListDrawerSettings(OnBeginListElementGUI = "DrawLabelForTask")]
    [SerializeField]
    private PrepareTask[] tasks;

    private void OnEnable()
    {
        this.gameContext.OnGameConstructed += this.PrepareGame;
    }

    private void OnDisable()
    {
        this.gameContext.OnGameConstructed -= this.PrepareGame;
    }

    private void PrepareGame()
    {
        for (int i = 0, count = this.tasks.Length; i < count; i++)
        {
            var task = this.tasks[i];
            task.Prepare(this.gameContext);
        }
    }

#if UNITY_EDITOR
    private void DrawLabelForTask(int index)
    {
        GUILayout.Space(4);
        GUILayout.Label($"Task #{index + 1}");
    }
#endif
}