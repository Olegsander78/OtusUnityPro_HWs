using UnityEngine;


[CreateAssetMenu(
    fileName = "EarnMoneyMission",
    menuName = MissionExtensions.MENU_PATH + "New EarnMoneyMission"
)]
public sealed class EarnMoneyMissionConfig : MissionConfig
{
    public int RequiredMoney
    {
        get { return this.requiredMoney; }
    }

    [Header("Quest")]
    [SerializeField]
    private int requiredMoney;

    public override Mission InstantiateMission()
    {
        return new EarnMoneyMission(config: this);
    }

    public override string Serialize(Mission mission)
    {
        var myMission = (EarnMoneyMission)mission;
        return myMission.EarnedMoney.ToString();
    }

    public override void DeserializeTo(string serializedData, Mission mission)
    {
        int.TryParse(serializedData, out var collectedResources);
        var myMission = (EarnMoneyMission)mission;
        myMission.Setup(collectedResources);
    }
}