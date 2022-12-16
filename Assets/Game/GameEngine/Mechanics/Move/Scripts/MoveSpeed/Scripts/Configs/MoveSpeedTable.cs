using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class MoveSpeedTable
{
    public float SpeedStep
    {
        get { return this.speedStep; }
    }

    [Space]
    [InfoBox("Speed: Linear Function")]
    [SerializeField]
    private float startSpeed;

    [SerializeField]
    private float endSpeed;

    [ReadOnly]
    [SerializeField]
    private float speedStep;

    [Space]
    //[ReadOnlyArray]
    [ListDrawerSettings(
        IsReadOnly = true,
        OnBeginListElementGUI = "DrawLabelForListElement"
    )]
    [SerializeField]
    private float[] table;

    public float GetSpeed(int level)
    {
        var index = Mathf.Clamp(level - 1, 0, this.table.Length);
        return this.table[index];
    }

    public void OnValidate(int maxLevel)
    {
        this.EvaluateTable(maxLevel);
    }

    private void EvaluateTable(int maxLevel)
    {
        this.table = new float[maxLevel];
        this.table[0] = this.startSpeed;
        this.table[maxLevel - 1] = this.endSpeed;

        var speedStep = (this.endSpeed - this.startSpeed) / (maxLevel - 1);
        this.speedStep = (float)Math.Round(speedStep, 2);

        for (var i = 1; i < maxLevel - 1; i++)
        {
            var speed = this.startSpeed + this.speedStep * i;
            this.table[i] = (float)Math.Round(speed, 2);
        }
    }

#if UNITY_EDITOR
    private void DrawLabelForListElement(int index)
    {
        GUILayout.Space(8);
        GUILayout.Label($"Level {index + 1}");
    }
#endif
}