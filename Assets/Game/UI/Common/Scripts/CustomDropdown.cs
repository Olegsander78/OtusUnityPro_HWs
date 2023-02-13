using TMPro;
using UnityEngine;


public sealed class CustomDropdown : TMP_Dropdown
{
    [SerializeField]
    private GameObject blocker;

    protected override GameObject CreateBlocker(Canvas rootCanvas)
    {
        var blocker = Instantiate(this.blocker, rootCanvas.transform);
        return blocker;
    }
}