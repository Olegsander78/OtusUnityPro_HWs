using System;
using JetBrains.Annotations;

namespace GameSystem
{
    ///FOR ADVANCED GAME ARCHITECTURE
    [MeansImplicitUse(ImplicitUseKindFlags.Default)]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class GameComponentAttribute : Attribute
    {
        public BindingType flags;

        public GameComponentAttribute()
        {
            this.flags = BindingType.NONE;
        }
        
        public GameComponentAttribute(BindingType flags)
        {
            this.flags = flags;
        }

        public bool FlagsExists(BindingType flags)
        {
            return (this.flags & flags) == flags;
        }
    }
}