using UnityEngine;


public sealed class LoadingScreen : MonoBehaviour
{
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}