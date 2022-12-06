using Entities;
using UnityEngine;
using GameElements;

public class DamageParameterAdapter : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement

{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        SetupPanel();
    }
    void IGameStartElement.StartGame(IGameContext context)
    {
        _character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged += UpdatePanel;

    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged -= UpdatePanel;
    }

    private void SetupPanel()
    {
        var damage = _character.Get<IComponent_GetMeleeDamage>().Damage;
        _panel.SetupValue(damage.ToString());
    }

    private void UpdatePanel(int newDamage)
    {
        _panel.UpdateValue(newDamage.ToString());
    }
}
