using UnityEngine;
using GameElements;

//ADAPTER
public sealed class MoneyPanelAdapter : MonoBehaviour,
    IGameInitElement, 
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private CurrencyPanel _moneyPanel;

    private MoneyStorage _moneyStorage;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
        _moneyPanel.SetupMoney(_moneyStorage.Money.ToString());
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _moneyStorage.OnMoneyChanged += OnMoneyChanged;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _moneyStorage.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int money)
    {
        _moneyPanel.UpdateMoney(money.ToString());
    }
}

