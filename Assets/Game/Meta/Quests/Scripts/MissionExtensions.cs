
public static class MissionExtensions
{
    public const string MENU_PATH = "Meta/Missions/";

    public const string MISSION_CATALOG_RESOURCE_PATH = "MissionCatalog";

    public static string GetDifficultyLocalizationKey(MissionDifficulty difficulty)
    {
        return $"Missions|{difficulty.ToString().ToUpper()}";
    }
}