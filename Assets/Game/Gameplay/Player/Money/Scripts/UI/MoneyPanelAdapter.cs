using UnityEngine;
using GameSystem;

//ADAPTER
public sealed class MoneyPanelAdapter : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement, 
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private CurrencyPanel _moneyPanel;

    private MoneyStorage _moneyStorage;

    void IGameInitElement.InitGame()
    {        
        _moneyPanel.SetupMoney(_moneyStorage.Money.ToString());
    }

    void IGameStartElement.StartGame()
    {
        _moneyStorage.OnMoneyChanged += OnMoneyChanged;
    }

    void IGameFinishElement.FinishGame()
    {
        _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _moneyPanel.UpdateMoney(money.ToString());
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
    }
}

