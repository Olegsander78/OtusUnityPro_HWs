using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//VIEW
public sealed class ExperiencePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelText;

    [SerializeField]
    private TextMeshProUGUI _expText;

    [SerializeField]
    private Image _iconPartyMember;

    public void SetupExp(string curExp, string maxExp)
    {
        _expText.text = $"EXP {curExp}/{maxExp}";
    }
    public void SetupLevel(string level)
    {
        _levelText.text = level;
    }

    public void SetupIcon(Sprite icon)
    {
        _iconPartyMember.sprite = icon;
    }

    public void UpdateExp(string curExp, string maxExp)
    {        
        _expText.text = $"EXP {curExp}/{maxExp}";
        AnimateTextBounce();
    }
    public void UpdateLevel(string level)
    {
        _levelText.text = level;
        AnimateLevelTextBounce();
    }

    private void AnimateTextBounce()
    {
        //Scale animation:
        DOTween
            .Sequence()
            .Append(_expText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
            .Append(_expText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
    }
    private void AnimateLevelTextBounce()
    {
        //Scale animation:
        DOTween
            .Sequence()
            .Append(_levelText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
            .Append(_levelText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
    }
}

