using System;
using System.Collections.Generic;
using System.Linq;

namespace GameSystem
{
    internal sealed class ElementContext
    {
        internal event Action<IGameElement> OnRegistered;

        internal event Action<IGameElement> OnUnregistered; 

        private readonly IGameContext gameContext;

        private readonly HashSet<IGameElement> gameElements;

        private readonly List<IGameElement> cache;
        
        internal ElementContext(IGameContext gameContext)
        {
            this.gameContext = gameContext;
            this.gameElements = new HashSet<IGameElement>();
            this.cache = new List<IGameElement>();
        }
        
        internal void AddElement(IGameElement element)
        {
            if (element == null)
            {
                return;
            }
            
            var addedElements = new HashSet<IGameElement>();
            this.AddRecursively(element, ref addedElements);
            
            foreach (var addedElement in addedElements)
            {
                this.TryActivateElement(addedElement);
                this.OnRegistered?.Invoke(addedElement);
            }
        }

        internal void RemoveElement(IGameElement element)
        {
            if (element != null)
            {
                this.RemoveRecursively(element);
            }
        }
        
        internal object[] GetAllElements()
        {
            return this.gameElements.ToArray<object>();
        }

        internal void ConstructGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameConstructElement constructElement)
                {
                    constructElement.ConstructGame(this.gameContext);
                }
            }
        }

        internal void InitGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);

            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameInitElement initElement)
                {
                    initElement.InitGame();
                }
            }
        }

        internal void ReadyGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameReadyElement initElement)
                {
                    initElement.ReadyGame();
                }
            }
        }

        internal void StartGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameStartElement startElement)
                {
                    startElement.StartGame();
                }
            }
        }

        internal void PauseGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGamePauseElement startElement)
                {
                    startElement.PauseGame();
                }
            }
        }

        internal void ResumeGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameResumeElement startElement)
                {
                    startElement.ResumeGame();
                }
            }
        }

        internal void FinishGame()
        {
            this.cache.Clear();
            this.cache.AddRange(this.gameElements);
            
            for (int i = 0, count = this.cache.Count; i < count; i++)
            {
                var element = this.cache[i];
                if (element is IGameFinishElement finishElement)
                {
                    finishElement.FinishGame();
                }
            }
        }

        private void AddRecursively(IGameElement element, ref HashSet<IGameElement> addedElements)
        {
            if (this.IsListeneableElement(element) && this.gameElements.Add(element))
            {
                addedElements.Add(element);
            }
            
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.AddRecursively(child, ref addedElements);
                }
            }
        }

        private bool IsListeneableElement(IGameElement element)
        {
            return element is
                IGameAttachElement or
                IGameDetachElement or
                IGameConstructElement or
                IGameInitElement or
                IGameReadyElement or
                IGameStartElement or
                IGamePauseElement or
                IGameResumeElement or
                IGameFinishElement;
        }

        private void RemoveRecursively(IGameElement element)
        {
            if (this.IsListeneableElement(element))
            {
                this.RemoveElementInternal(element);
            }
            
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.RemoveRecursively(child);
                }
            }
        }

        private void RemoveElementInternal(IGameElement element)
        {
            this.gameElements.Remove(element);
            if (element is IGameDetachElement detachElement)
            {
                detachElement.DetachGame(this.gameContext);
            }
            
            this.OnUnregistered?.Invoke(element);
        }

        private void TryActivateElement(IGameElement element)
        {
            if (element is IGameAttachElement attachElement)
            {
                attachElement.AttachGame(this.gameContext);
            }
            
            var gameState = this.gameContext.State;
            if (gameState >= GameState.FINISH)
            {
                return;
            }

            if (gameState < GameState.CONSTRUCT)
            {
                return;
            }
            
            if (element is IGameConstructElement constructElement)
            {
                constructElement.ConstructGame(this.gameContext);
            }

            if (gameState < GameState.INIT)
            {
                return;
            }

            if (element is IGameInitElement initElement)
            {
                initElement.InitGame();
            }

            if (gameState < GameState.READY)
            {
                return;
            }

            if (element is IGameReadyElement readyElement)
            {
                readyElement.ReadyGame();
            }
            
            if (gameState < GameState.PLAY)
            {
                return;
            }

            if (element is IGameStartElement startElement)
            {
                startElement.StartGame();
            }
            
            if (gameState == GameState.PAUSE && element is IGamePauseElement pauseElement)
            {
                pauseElement.PauseGame();
            }
        }
    }
}