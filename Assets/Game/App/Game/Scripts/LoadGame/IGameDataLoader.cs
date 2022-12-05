using GameElements;

//Интерфейс для загрузки данных в игру
public interface IGameDataLoader
{
    void LoadData(IGameContext context);
}