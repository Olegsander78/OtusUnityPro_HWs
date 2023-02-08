//using System.Collections;
//using System.Threading.Tasks;
//using Asyncoroutine;
//using Services;
//using SystemTime;


//public sealed class RealtimeStarter
//{
//    [Inject]
//    private RealtimeManager realtimeManager;

//    [Inject]
//    private RealtimeRepository repository;

//    public async Task StartSessionAsync()
//    {
//        if (this.repository.LoadSession(out RealtimeData previousSession))
//        {
//            await this.StartSessionByPrevious(previousSession.nowSeconds);
//        }
//        else
//        {
//            await this.StartSessionAsFirst();
//        }
//    }

//    private IEnumerator StartSessionByPrevious(long previousSeconds)
//    {
//        yield return OnlineTime.RequestNowSeconds(nowSeconds =>
//        {
//            var pauseTime = nowSeconds - previousSeconds;
//            this.realtimeManager.Begin(nowSeconds, pauseTime);
//        });
//    }

//    private IEnumerator StartSessionAsFirst()
//    {
//        yield return OnlineTime.RequestNowSeconds(nowSeconds =>
//        {
//            this.realtimeManager.Begin(nowSeconds);
//        });
//    }
//}