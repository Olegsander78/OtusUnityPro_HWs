using System;


public interface ILoadingTask
{
    void Do(Action<LoadingResult> callback);
}


