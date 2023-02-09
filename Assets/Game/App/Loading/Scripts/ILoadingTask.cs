using System;
using System.Threading.Tasks;


public interface ILoadingTask
{
    void Do(Action<LoadingResult> callback);
}


