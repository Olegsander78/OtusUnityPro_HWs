using UnityEngine;


public sealed class DestroyMechanics_HideGameObjects : DestroyMechanics
{
    [SerializeField]
    private GameObject[] gameObjects;

    protected override void OnDestroyEvent(DestroyEvent destroyEvent)
    {
        for (int i = 0, count = this.gameObjects.Length; i < count; i++)
        {
            var gameObject = this.gameObjects[i];
            gameObject.SetActive(false);
        }
    }
}