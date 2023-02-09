using System.Collections.Generic;

namespace GameSystem
{
    public interface IGameElement
    {
    }
    
    ///Game lifecycle
    public interface IGameAttachElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when this element has registered to a game system.</para>
        /// </summary>
        void AttachGame(IGameContext context);
    }

    public interface IGameDetachElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when this element has unregistered from a game system.</para>
        /// </summary>
        void DetachGame(IGameContext context);
    }

    public interface IGameConstructElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when need to resolve dependencies.</para>
        ///     <para>Do not use this interface for advanced architecture</para>
        ///     <seealso cref="GameInjector"/>
        ///     <seealso cref="GameInjectAttribute"/>
        /// </summary>
        void ConstructGame(IGameContext context);
    }

    public interface IGameInitElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is initialized.</para>
        /// </summary>
        void InitGame();
    }

    public interface IGameReadyElement : IGameElement
    {
        /// <summary>
        ///     <para>Calls when a game system is ready.</para>
        /// </summary>
        void ReadyGame();
    }

    public interface IGameStartElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is started.</para>
        /// </summary>
        void StartGame();
    }

    public interface IGamePauseElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is paused.</para>
        /// </summary>
        void PauseGame();
    }

    public interface IGameResumeElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is resumed</para>
        /// </summary>
        void ResumeGame();
    }

    public interface IGameFinishElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is finished.</para>
        /// </summary>
        void FinishGame();
    }

    public interface IGameElementGroup : IGameElement
    {
        /// <summary>
        ///     <para>Returns a collection of elements.</para>
        /// </summary>
        IEnumerable<IGameElement> GetElements();
    }
}