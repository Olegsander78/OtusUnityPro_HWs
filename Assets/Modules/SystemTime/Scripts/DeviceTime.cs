using System;
using UnityEngine;

namespace SystemTime
{
    public static class DeviceTime
    {
        public static long GetSystemUpTime()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			AndroidJavaClass systemClock = new AndroidJavaClass("android.os.SystemClock");
			return Mathf.FloorToInt(systemClock.CallStatic<long>("elapsedRealtime") / 1000f);
#elif UNITY_IOS && !UNITY_EDITOR
			return IOSTime.GetSystemUpTime() / 1000;
#else
            var deviceRunTimeTicks = Environment.TickCount & int.MaxValue;
            var totalSeconds = Mathf.FloorToInt(deviceRunTimeTicks / 1000f);
            return totalSeconds;
#endif
        }
    }
}