namespace UIFrames
{
    public interface IFrameFactory<in TKey, out TValue> where TValue : IFrame
    {
        TValue CreateFrame(TKey key);
    }
}