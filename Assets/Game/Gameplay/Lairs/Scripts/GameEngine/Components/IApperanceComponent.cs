using System;


public interface IAppearanceComponent
{
    event Action OnStartWork;

    event Action<float> OnProgress;

    event Action OnFinishWork;

    bool IsWorking { get; }

    float Progress { get; }
}