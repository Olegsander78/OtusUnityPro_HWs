using UnityEngine;


[CreateAssetMenu(
    fileName = "KillEnemyMission",
    menuName = MissionExtensions.MENU_PATH + "New KillEnemyMission"
)]
public sealed class KillEnemyMissionConfig : MissionConfig
{
    public int RequiredKills
    {
        get { return this.requiredKills; }
    }

    [Header("Quest")]
    [SerializeField]
    private int requiredKills;

    public override Mission InstantiateMission()
    {
        return new KillEnemyMission(this);
    }

    public override string Serialize(Mission mission)
    {
        var myMission = (KillEnemyMission)mission;
        return myMission.CurrentKills.ToString();
    }

    public override void DeserializeTo(string serializedData, Mission mission)
    {
        int.TryParse(serializedData, out var currentKills);
        var myMission = (KillEnemyMission)mission;
        myMission.Setup(currentKills);
    }
}