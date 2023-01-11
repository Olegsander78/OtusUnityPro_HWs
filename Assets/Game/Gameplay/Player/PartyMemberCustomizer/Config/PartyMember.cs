using Entities;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Player",
    menuName = "MetaGame - Products/New PartyMember (Presentation Model)"
)]
public sealed class PartyMember : ScriptableObject
{
    [PreviewField]
    [SerializeField]
    public Sprite IconHeroImage;

    [SerializeField]
    public string TitleText;

    [SerializeField]
    public string NameHeroText;

    [SerializeField]
    public PartyMemberClass ClassHero;

    [TextArea]
    [SerializeField]
    public string HistoryHeroText;

    //[SerializeField]
    //public UnityEntityBase MemberOfParty;
}
