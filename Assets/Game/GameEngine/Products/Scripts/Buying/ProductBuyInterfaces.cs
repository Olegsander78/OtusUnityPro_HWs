
    public interface IProductBuyCondition
    {
        bool CanBuy(Product product);
    }
        
    public interface IProductBuyProcessor
    {
        void ProcessBuy(Product product);
    }

public interface IProductBuyCompletor
{
    void CompleteBuy(Product product);
}