
public interface IComponent_MoneyPrice
{
    int Price { get; }
}

public interface IComponent_ResourcePrice
{
    ResourceData[] GetPrice();
}