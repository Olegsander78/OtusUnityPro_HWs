using DG.Tweening;
using TMPro;
using UnityEngine;

//VIEW
public sealed class CurrencyPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _moneyText;

    public void SetupMoney(string money)
    {
        _moneyText.text = money;
    }

    public void UpdateMoney(string money)
    {
        _moneyText.text = money;
        AnimateTextBounce();
    }

    private void AnimateTextBounce()
    {
        //Scale animation:
        DOTween
            .Sequence()
            .Append(_moneyText.transform.DOScale(new Vector3(1.1f, 1.1f, 1.0f), 0.1f))
            .Append(_moneyText.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.3f));
    }
}

