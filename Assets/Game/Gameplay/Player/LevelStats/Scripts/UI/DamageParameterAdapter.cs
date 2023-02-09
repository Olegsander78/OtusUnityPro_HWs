using Entities;
using UnityEngine;
using GameSystem;

public class DamageParameterAdapter : MonoBehaviour,
    IGameConstructElement,
    IGameStartElement,
    IGameFinishElement

{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        SetupPanel();
    }
    void IGameStartElement.StartGame()
    {
        _character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged += UpdatePanel;

    }

    void IGameFinishElement.FinishGame()
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
