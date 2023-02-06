#if UNITY_IOS
using System.Runtime.InteropServices;

namespace SystemTime
{

    public static class IOSTime
    {
        [DllImport("__Internal")]
        public static extern long GetSystemUpTime();
    }

}
#endif