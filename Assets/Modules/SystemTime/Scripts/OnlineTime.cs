using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

namespace SystemTime
{
    public static class OnlineTime
    {
        private static readonly DateTime originTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     <para>Load current time in seconds (UTC).</para> 
        /// </summary>
        public static IEnumerator RequestNowSeconds(Action<long> callback)
        {
            var timeLoader = new OnlineTimeLoader();
            yield return timeLoader.RequestNowTime();

            var response = timeLoader.Result;
            DateTime nowTime;

            if (response.isSuccess)
            {
                nowTime = response.time;
            }
            else
            {
                nowTime = DateTime.Now.ToUniversalTime();
            }

            TimeSpan timeSpan = nowTime - originTime;
            var nowSeconds = (long) timeSpan.TotalSeconds;
            callback?.Invoke(nowSeconds);
        }
    }

    /// <summary>
    ///     Делает запрос по актуальному времени на microsoft.com.
    /// </summary>
    public sealed class OnlineTimeLoader
    {
        #region Const

        private const int BREAK_TIME_DEFAULT = 2;

        private const string URL = "https://www.microsoft.com";

        #endregion

        public Response Result { get; private set; }

        public IEnumerator RequestNowTime()
        {
            var request = new UnityWebRequest(URL)
            {
                downloadHandler = new DownloadHandlerBuffer(),
                timeout = BREAK_TIME_DEFAULT
            };

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                var error = $"Downloading time stopped: {request.error}";
                this.Result = new Response(isSuccess: false, error: error, time: default);
                yield break;
            }

            if (request.downloadHandler == null)
            {
                const string error = "Downloading time stopped: DownloadHandler is NULL";
                this.Result = new Response(isSuccess: false, error: error, time: default);
                yield break;
            }

            if (string.IsNullOrEmpty(request.downloadHandler.text))
            {
                const string error = "Downloading time stopped: Downloaded string is empty or NULL";
                this.Result = new Response(isSuccess: false, error: error, time: default);
                yield break;
            }

            var dateStr = request.GetResponseHeaders()["date"];
            var time = DateTime.ParseExact(
                dateStr,
                "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AdjustToUniversal
            );

            this.Result = new Response(isSuccess: true, error: "", time: time);
            yield return new WaitForEndOfFrame();
        }

        public readonly struct Response
        {
            public readonly bool isSuccess;
            public readonly string error;
            public readonly DateTime time;

            public Response(bool isSuccess, string error, DateTime time)
            {
                this.isSuccess = isSuccess;
                this.error = error;
                this.time = time;
            }
        }
    }
}