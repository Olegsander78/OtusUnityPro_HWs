using System.Collections.Generic;

namespace GameElements
{
    public interface IGameElement
    {
    }
    
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

    ///Game lifecycle
    public interface IGameInitElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is initialized.</para>
        /// </summary>
        void InitGame(IGameContext context);
    }

    public interface IGameReadyElement : IGameElement
    {
        /// <summary>
        ///     <para>Calls when a game system is ready.</para>
        /// </summary>
        void ReadyGame(IGameContext context);
    }

    public interface IGameStartElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is started.</para>
        /// </summary>
        void StartGame(IGameContext context);
    }

    public interface IGamePauseElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is paused.</para>
        /// </summary>
        void PauseGame(IGameContext context);
    }

    public interface IGameResumeElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is resumed</para>
        /// </summary>
        void ResumeGame(IGameContext context);
    }

    public interface IGameFinishElement : IGameElement
    {
        /// <summary>
        ///     <para>Called when a game system is finished.</para>
        /// </summary>
        void FinishGame(IGameContext context);
    }

    public interface IGameElementGroup : IGameElement
    {
        /// <summary>
        ///     <para>Returns a collection of elements.</para>
        /// </summary>
        IEnumerable<IGameElement> GetElements();
    }
}