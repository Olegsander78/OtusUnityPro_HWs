using GameElements;

//Интерфейс для сохранения данных из игры
public interface IGameDataSaver
{
    void SaveData(IGameContext context);
}