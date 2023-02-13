using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public class ProductBuyer
{
    public event Action<Product> OnStarted;

    public event Action<Product> OnCompleted;

    private readonly List<IProductBuyCondition> conditions;

    private readonly List<IProductBuyProcessor> processors;

    private readonly List<IProductBuyCompletor> completors;

    public ProductBuyer()
    {
        this.conditions = new List<IProductBuyCondition>();
        this.processors = new List<IProductBuyProcessor>();
        this.completors = new List<IProductBuyCompletor>();
    }

    public bool CanBuyProduct(Product product)
    {
        for (int i = 0, count = this.conditions.Count; i < count; i++)
        {
            var condition = this.conditions[i];
            if (!condition.CanBuy(product))
            {
                return false;
            }
        }

        return true;
    }

    [Button]
    public void BuyProduct(ProductConfig config)
    {
        this.BuyProduct(config.Prototype);
    }

    public void BuyProduct(Product product)
    {
        if (!this.CanBuyProduct(product))
        {
            Debug.LogWarning($"Can't buy product {product.Id}!");
            return;
        }

        this.OnStarted?.Invoke(product);

        //Process buy:
        for (int i = 0, count = this.processors.Count; i < count; i++)
        {
            var processor = this.processors[i];
            processor.ProcessBuy(product);
        }

        //Complete buy:
        for (int i = 0, count = this.completors.Count; i < count; i++)
        {
            var completor = this.completors[i];
            completor.CompleteBuy(product);
        }

        this.OnCompleted?.Invoke(product);
    }

    public void AddCondition(IProductBuyCondition condition)
    {
        this.conditions.Add(condition);
    }

    public void RemoveChecker(IProductBuyCondition condition)
    {
        this.conditions.Remove(condition);
    }

    public void AddProcessor(IProductBuyProcessor processor)
    {
        this.processors.Add(processor);
    }

    public void RemoveProcessor(IProductBuyProcessor processor)
    {
        this.processors.Remove(processor);
    }

    public void AddCompletor(IProductBuyCompletor completor)
    {
        this.completors.Add(completor);
    }

    public void RemoveCompletor(IProductBuyCompletor completor)
    {
        this.completors.Remove(completor);
    }
}