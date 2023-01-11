using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class MeleeDamageUpgradeTable
{
    public int DamageStep
    {
        get { return this.damageStep; }
    }

    [InfoBox("Damage: Linear Function")]
    [SerializeField]
    private int startDamage = 1;

    [SerializeField]
    private int damageStep = 1;

    [Space]
    [ReadOnly]
    [ListDrawerSettings(
        IsReadOnly = true,
        OnBeginListElementGUI = "DrawLabelForListElement"
    )]
    [SerializeField]
    private int[] levels;

    public int GetDamage(int level)
    {
        var index = level - 1;
        return this.levels[index];
    }

    public void OnValidate(int maxLevel)
    {
        this.levels = new int[maxLevel];

        var currentDamage = this.startDamage;
        for (var i = 0; i < maxLevel; i++)
        {
            this.levels[i] = currentDamage;
            currentDamage += this.damageStep;
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