using Elementary;
using UnityEngine;


    public sealed class ActionBehaviour_StopHarvestResource : ActionBehaviour
    {
        [SerializeField]
        private HarvestResourceEngineLS engine;
    
        public override void Do()
        {
            if (this.engine.IsHarvesting)
            {
                this.engine.StopHarvest();
            }
        }
    }