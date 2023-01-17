namespace UIFrames
{
    public interface IFrameSupplier<in TKey, TValue> where TValue : IFrame
    {
        TValue LoadFrame(TKey key);

        void UnloadFrame(TValue frame);
    }
}