using System.Collections;
using UnityEngine;


public sealed class BoosterViewAdapter
{
    private readonly BoosterView view;

    private readonly Booster booster;

    private readonly MonoBehaviour coroutineDispatcher;

    private Coroutine timeCoroutine;

    public BoosterViewAdapter(BoosterView view, Booster booster, MonoBehaviour coroutineDispatcher)
    {
        this.view = view;
        this.booster = booster;
        this.coroutineDispatcher = coroutineDispatcher;
    }

    public void Show()
    {
        var metadata = this.booster.Metadata;
        this.view.SetIcon(metadata.icon);
        this.view.SetLabel(metadata.viewLabel);
        this.view.SetColor(metadata.viewColor);
        this.view.SetRemainingTime(this.booster.RemainingTime, this.booster.Duration);

        this.timeCoroutine = this.coroutineDispatcher.StartCoroutine(this.UpdateTimeRoutine());
    }

    public void Hide()
    {
        if (this.timeCoroutine != null)
        {
            this.coroutineDispatcher.StopCoroutine(this.timeCoroutine);
            this.timeCoroutine = null;
        }
    }

    private IEnumerator UpdateTimeRoutine()
    {
        var period = new WaitForSeconds(1);
        while (true)
        {
            yield return period;
            this.view.SetRemainingTime(this.booster.RemainingTime, this.booster.Duration);
        }
    }
}