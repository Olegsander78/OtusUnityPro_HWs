using Elementary;
using UnityEngine;


public sealed class ActivationMechanics_SetActiveGameObjects : MonoBehaviour
{
    [SerializeField]
    private ActivationBehaviour toggle;

    [Space]
    [SerializeField]
    private GameObject[] gameObjects;

    private void Awake()
    {
        this.SetActive(this.toggle.IsActive);
    }

    private void OnEnable()
    {
        this.toggle.OnActive += this.SetActive;
        this.toggle.OnActivate += this.Activate;
        this.toggle.OnDeactivate += this.Deactivate;
    }

    private void OnDisable()
    {
        this.toggle.OnActive -= this.SetActive;
        this.toggle.OnDeactivate -= this.Deactivate;
        this.toggle.OnDeactivate -= this.Deactivate;
    }

    private void Activate()
    {
        this.SetActive(true);
    }

    private void Deactivate()
    {
        this.SetActive(false);
    }

    private void SetActive(bool isActive)
    {
        for (int i = 0, count = this.gameObjects.Length; i < count; i++)
        {
            var gameObject = this.gameObjects[i];
            gameObject.SetActive(isActive);
        }
    }
}