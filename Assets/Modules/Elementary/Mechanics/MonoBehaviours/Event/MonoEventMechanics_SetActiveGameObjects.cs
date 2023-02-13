using System.Collections;
using UnityEngine;

namespace Elementary
{
    [AddComponentMenu("Elementary/Mechanics/Event Mechanics «Set Active Game Objects»")]
    public sealed class MonoEventMechanics_SetActiveGameObjects : MonoEventMechanics
    {
        [Space]
        [SerializeField]
        private float hideDelay = 1.25f;
        
        [SerializeField]
        private bool setActive = true;
        
        [SerializeField]
        private GameObject[] gameObjects;

        protected override void OnEvent()
        {
            this.StartCoroutine(this.EnableObjects());
        }

        private IEnumerator EnableObjects()
        {
            yield return new WaitForSeconds(this.hideDelay);
            for (int i = 0, count = this.gameObjects.Length; i < count; i++)
            {
                var visual = this.gameObjects[i];
                visual.SetActive(this.setActive);
            }
        }
    }
}