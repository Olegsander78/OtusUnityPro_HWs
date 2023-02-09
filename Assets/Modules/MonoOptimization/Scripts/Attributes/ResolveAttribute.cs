using System;
using JetBrains.Annotations;

namespace MonoOptimization
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Parameter)]
    public sealed class ResolveAttribute : Attribute
    {
    }
}