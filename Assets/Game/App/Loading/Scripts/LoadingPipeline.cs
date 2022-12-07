using System;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(
    fileName = "LoadingPipeline",
    menuName = "App/AppLoading/New LoadingPipeline"
)]
public sealed class LoadingPipeline : ScriptableObject
{
    [SerializeField]
    [FormerlySerializedAs("taskInfos")]
    [ListDrawerSettings(OnBeginListElementGUI = "DrawLabelForTask")]
    private TaskInfo[] tasks;

    public ILoadingTask[] GetTasks()
    {
        var count = this.tasks.Length;
        var result = new ILoadingTask[count];
        for (var i = 0; i < count; i++)
        {
            var taskInfo = this.tasks[i];
            var task = CreateTask(taskInfo);
            result[i] = task;
        }

        return result;
    }

    [Serializable]
    private sealed class TaskInfo
    {
#if UNITY_EDITOR
        [SerializeField]
        public MonoScript script;
#endif
        [HideInInspector]
        [SerializeField]
        public string className;
    }

    private static ILoadingTask CreateTask(TaskInfo taskInfo)
    {
        var classType = Type.GetType(taskInfo.className);
        if (classType == null)
        {
            throw new Exception($"Missed class {taskInfo.className} of MonoScript {taskInfo.script.name}");
        }

        var task = (ILoadingTask)Activator.CreateInstance(classType);
        return task;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (var info in this.tasks)
        {
            if (info.script != null)
            {
                info.className = info.script.GetClass().FullName;
            }
        }
    }

    private void DrawLabelForTask(int index)
    {
        GUILayout.Space(4);
        GUILayout.Label($"Task #{index + 1}");
    }
#endif
}