
public readonly struct LoadingResult
{
    public readonly bool success;

    public readonly string error;

    private LoadingResult(bool success, string error)
    {
        this.success = success;
        this.error = error;
    }

    public static LoadingResult Success()
    {
        return new LoadingResult(true, null);
    }

    public static LoadingResult Fail(string error)
    {
        return new LoadingResult(false, error);
    }
}