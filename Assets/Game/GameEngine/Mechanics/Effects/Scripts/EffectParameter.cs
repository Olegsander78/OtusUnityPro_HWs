using System;
using UnityEngine;


    public interface IEffectParameter
    {
        EffectParameterKey Name { get; }
    }

    public interface IEffectParameter<out T> : IEffectParameter
    {
        T Value { get; }
    }

    [Serializable]
    public abstract class AbstractEffectParameter<T> : IEffectParameter<T>
    {
        public EffectParameterKey Name
        {
            get { return this.name; }
        }

        public T Value
        {
            get { return this.value; }
        }

        [SerializeField]
        private EffectParameterKey name;

        [SerializeField]
        private T value;

        public AbstractEffectParameter()
        {
        }

        public AbstractEffectParameter(EffectParameterKey name, T value)
        {
            this.name = name;
            this.value = value;
        }
    }

    [Serializable]
    public sealed class IntEffectParameter : AbstractEffectParameter<int>
    {
        public IntEffectParameter()
        {
        }

        public IntEffectParameter(EffectParameterKey name, int value) : base(name, value)
        {
        }
    }

    [Serializable]
    public sealed class FloatEffectParameter : AbstractEffectParameter<float>
    {
        public FloatEffectParameter()
        {
        }

        public FloatEffectParameter(EffectParameterKey name, float value) : base(name, value)
        {
        }
    }

[Serializable]
public sealed class StringEffectParameter : AbstractEffectParameter<string>
{
    public StringEffectParameter()
    {
    }

    public StringEffectParameter(EffectParameterKey name, string value) : base(name, value)
    {
    }
}