using UnityEngine;

namespace GameSystem
{

    [CreateAssetMenu(
        fileName = "ConstructTaskGroup",
        menuName = "GameElements/New ConstructTaskGroup"
    )]
    public sealed class ConstructTaskGroup : ConstructTask
    {
        [SerializeField]
        private ConstructTask[] tasks;
        
        public override void Construct(IGameContext gameContext)
        {
            for (int i = 0, count = this.tasks.Length; i < count; i++)
            {
                var task = this.tasks[i];
                task.Construct(gameContext);
            }
        }
    }
}