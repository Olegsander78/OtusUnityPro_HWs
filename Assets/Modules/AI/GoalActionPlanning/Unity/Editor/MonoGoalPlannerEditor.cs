#if UNITY_EDITOR
using System.Linq;
using AI.GOAP.Unity;
using UnityEditor;
using UnityEngine;

namespace AI.GOAP.UnityEditor
{
    [CustomEditor(typeof(BaseGoalPlanner))]
    public sealed class MonoGoalPlannerEditor : Editor
    {
        private BaseGoalPlanner planner;

        private void Awake()
        {
            this.planner = (BaseGoalPlanner) this.target;
        }

        public override void OnInspectorGUI()
        {
            if (EditorApplication.isPlaying)
            {
                this.DrawPlaymode();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }

        private void DrawPlaymode()
        {
            GUI.enabled = false;
            this.DrawGoals();
            this.DrawActions();
            GUI.enabled = true;

            EditorGUILayout.Space(8.0f);
            if (GUILayout.Button("Debug Plan"))
            {
                this.DebugPlan();
            }
        }

        private void DrawGoals()
        {
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Active Goals");
            var goals = this.planner.Editor_GetGoals()
                .OrderByDescending(it => it.IsValid() ? it.EvaluatePriority() : -1);

            foreach (var goal in goals)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(goal, typeof(AbstractGoal), false);
                var isValid = goal.IsValid();
                if (isValid)
                {
                    EditorGUILayout.TextField("Priority: " + goal.EvaluatePriority());
                }
                else
                {
                    EditorGUILayout.TextField("Inactive");
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawActions()
        {
            EditorGUILayout.Space(4.0f);
            EditorGUILayout.LabelField("Active Actions");
            var actions = this.planner.Editor_GetActions();

            foreach (var action in actions)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.ObjectField(action, typeof(AbstractAction), false);
                var isValid = action.IsValid();
                if (isValid)
                {
                    EditorGUILayout.TextField("Cost: " + action.EvaluateCost());
                }
                else
                {
                    EditorGUILayout.TextField("Inactive");
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private void DebugPlan()
        {
            if (this.planner.MakePlan(out var plan))
            {
                Debug.Log($"<color=#B1EEF1>Success {plan}</color>");
            }
            else
            {
                Debug.Log("<color=#B1EEF1>Fail</color>");

            }
        }
    }
}
#endif