using System;
using JetBrains.Annotations;

namespace Services
{
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class ServiceInjectAttribute : Attribute
    {
    }
}