using System.Collections.Generic;

namespace GameElements
{
    internal sealed class ElementContext
    {
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
            }
        }

        internal void RemoveElement(IGameElement element)
        {
            if (element != null)
            {
                this.RemoveRecursively(element);
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
                    initElement.InitGame(this.gameContext);
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
                    initElement.ReadyGame(this.gameContext);
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
                    startElement.StartGame(this.gameContext);
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
                    startElement.PauseGame(this.gameContext);
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
                    startElement.ResumeGame(this.gameContext);
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
                    finishElement.FinishGame(this.gameContext);
                }
            }
        }

        private void AddRecursively(IGameElement element, ref HashSet<IGameElement> addedElements)
        {
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.AddRecursively(child, ref addedElements);
                }
            }
            else
            {
                if (this.gameElements.Add(element))
                {
                    addedElements.Add(element);
                }
            }
        }

        private void RemoveRecursively(IGameElement element)
        {
            if (element is IGameElementGroup elementGroup)
            {
                foreach (var child in elementGroup.GetElements())
                {
                    this.RemoveRecursively(child);
                }
            }
            else
            {
                this.gameElements.Remove(element);
                if (element is IGameDetachElement detachElement)
                {
                    detachElement.DetachGame(this.gameContext);
                }
            }
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

            if (gameState < GameState.INIT)
            {
                return;
            }

            if (element is IGameInitElement initElement)
            {
                initElement.InitGame(this.gameContext);
            }

            if (gameState < GameState.READY)
            {
                return;
            }

            if (element is IGameReadyElement readyElement)
            {
                readyElement.ReadyGame(this.gameContext);
            }
            
            if (gameState < GameState.PLAY)
            {
                return;
            }

            if (element is IGameStartElement startElement)
            {
                startElement.StartGame(this.gameContext);
            }
            
            if (gameState == GameState.PAUSE && element is IGamePauseElement pauseElement)
            {
                pauseElement.PauseGame(this.gameContext);
            }
        }
    }
}