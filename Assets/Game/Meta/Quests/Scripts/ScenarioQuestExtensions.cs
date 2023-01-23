
public static class ScenarioQuestExtensions
{
    public const string MENU_PATH = "Meta/ScenarioQuests/";

    public const string MISSION_CATALOG_RESOURCE_PATH = "ScenarioQuestsCatalog";

    public static string GetStageLocalizationKey(ScenarioQuestStage stage)
    {
        return $"ScenarioQuest|{stage.ToString().ToUpper()}";
    }
}