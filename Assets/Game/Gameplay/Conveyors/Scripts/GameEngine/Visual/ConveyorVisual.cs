using UnityEngine;


public sealed class ConveyorVisual : MonoBehaviour
{
    private static readonly int STATE = Animator.StringToHash("State");

    private const int IDLE_ANIMATION = 0;

    private const int SAW_ANIMATION = 1;

    [Space]
    [SerializeField]
    private Animator workerAnimator;

    [SerializeField]
    private GameObject sawObject;

    [SerializeField]
    private GameObject woodObject;

    private void Awake()
    {
        this.sawObject.SetActive(false);
        this.woodObject.SetActive(false);
    }

    public void Play()
    {
        this.workerAnimator.SetInteger(STATE, SAW_ANIMATION);
        this.sawObject.SetActive(true);
        this.woodObject.SetActive(true);
    }

    public void Stop()
    {
        this.workerAnimator.SetInteger(STATE, IDLE_ANIMATION);
        this.sawObject.SetActive(false);
        this.woodObject.SetActive(false);
    }
}