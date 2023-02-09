using System.Collections;
using System.Threading.Tasks;
using Asyncoroutine;
using Services;
using SystemTime;
using UnityEngine;

public sealed class RealtimeSessionStarter
{
    [ServiceInject]
    private RealtimeManager _realtimeManager;

    [ServiceInject]
    private RealtimeRepository _repository;

    //[ServiceInject]
    //public void Construct(RealtimeManager realtimeManager, RealtimeRepository repository)
    //{
    //    _realtimeManager = realtimeManager;
    //    _repository = repository;
    //}

    public async Task StartSessionAsync()
    {
        if (this._repository.LoadSession(out RealtimeData previousSession))
        {
            await this.StartSessionByPrevious(previousSession.nowSeconds);
        }
        else
        {
            await this.StartSessionAsFirst();
        }
    }

    private IEnumerator StartSessionByPrevious(long previousSeconds)
    {
        yield return OnlineTime.RequestNowSeconds(nowSeconds =>
        {
            var pauseTime = nowSeconds - previousSeconds;
            this._realtimeManager.Begin(nowSeconds, pauseTime);
            Debug.Log($"{nowSeconds}, {pauseTime} - time session by previous");
        });
    }

    private IEnumerator StartSessionAsFirst()
    {
        yield return OnlineTime.RequestNowSeconds(nowSeconds =>
        {            
            this._realtimeManager.Begin(nowSeconds);
            Debug.Log($"{nowSeconds} - time first session");
        });
    }
}