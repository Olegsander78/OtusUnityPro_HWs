using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Player",
    menuName = "MetaGame - Products/New Player (Presentation Model)"
)]
public sealed class Player : ScriptableObject
{
    [PreviewField]
    [SerializeField]
    public Sprite IconHeroImage;

    [SerializeField]
    public string TitleText;

    [SerializeField]
    public string NameHeroText;

    [SerializeField]
    public PlayerClass ClassHero;

    [TextArea]
    [SerializeField]
    public string HistoryHeroText;    
}
