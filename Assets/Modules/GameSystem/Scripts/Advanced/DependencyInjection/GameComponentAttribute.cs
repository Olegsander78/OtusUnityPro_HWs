using System;
using JetBrains.Annotations;

namespace GameSystem
{
    [Flags]
    public enum GameComponentType
    {
        NONE = 0,
        ELEMENT = 1,
        SERVICE = 2
    }
    
    ///FOR ADVANCED GAME ARCHITECTURE
    [MeansImplicitUse(ImplicitUseKindFlags.Default)]
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class GameComponentAttribute : Attribute
    {
        public readonly GameComponentType type;

        public GameComponentAttribute()
        {
            this.type = GameComponentType.NONE;
        }
        
        public GameComponentAttribute(GameComponentType type)
        {
            this.type = type;
        }
    }
}