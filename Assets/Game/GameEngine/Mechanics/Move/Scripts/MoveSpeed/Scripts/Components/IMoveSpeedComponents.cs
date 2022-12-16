using System;


public interface IComponent_GetMoveSpeed
{
    float Speed { get; }
}

public interface IComponent_SetMoveSpeed
{
    void SetSpeed(float speed);
}

public interface IComponent_OnMoveSpeedChanged
{
    event Action<float> OnSpeedChanged;
}