using System;
using JetBrains.Annotations;

namespace MonoOptimization
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class MonoComponentAttribute : Attribute
    {
    }
}

