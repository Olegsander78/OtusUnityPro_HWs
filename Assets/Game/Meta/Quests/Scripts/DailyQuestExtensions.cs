
public static class DailyQuestExtensions
{
    public const string MENU_PATH = "Meta/DailyQuests/";

    public const string MISSION_CATALOG_RESOURCE_PATH = "DailyQuestsCatalog";

    public static string GetDifficultyLocalizationKey(DailyQuestDifficulty difficulty)
    {
        return $"DailyQuest|{difficulty.ToString().ToUpper()}";
    }
}