using Entities;
using UnityEngine;

public class DamageParameterAdapter : MonoBehaviour,
    IConstructListener,
    IStartGameListener,
    IFinishGameListener

{
    [SerializeField]
    private PropertyPanel _panel;

    private IEntity _character;

    public void Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        SetupPanel();
    }
    public void OnStartGame()
    {
        _character.Get<IComponent_OnMeleeDamageChanged>().OnDamageChanged += UpdatePanel;

    }

    public void OnFinishGame()
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
