using System;
using JetBrains.Annotations;

namespace GameSystem
{
    ///FOR ADVANCED GAME ARCHITECTURE
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method | AttributeTargets.Constructor)]
    public sealed class GameInjectAttribute : Attribute
    {
    }
}