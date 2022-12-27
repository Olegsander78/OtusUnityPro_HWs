using UnityEngine;
using Elementary;
using System;
using System.Collections;

public class RangeAttackEngine : MonoBehaviour
{
    public event Action OnRangeAttackStarted;

    public event Action OnRangeAttackFinished;

    public bool IsShoted => _isShoted;

    //[SerializeField]
    //private EventReceiver _rangeAttackReciever; 

    //[SerializeField]
    //private ProjectileEngine _projectileEngine;

    [Header("Shot Countdown")]
    [SerializeField]
    private TimerBehaviour _shotCountdown;

    [Header("Pre-Shot Countdown")]
    [SerializeField]
    private float _preshotCountdown;
        
    private bool _isShoted;

    private Coroutine _shotRoutine;


    private void OnEnable()
    {
        //_rangeAttackReciever.OnEvent += OnRequestRangeAttack;
        _shotCountdown.OnFinished += FinishAttack;
    }

    private void OnDisable()
    {
       // _rangeAttackReciever.OnEvent -= OnRequestRangeAttack;
        _shotCountdown.OnFinished -= FinishAttack;
    }
    public void OnRequestRangeAttack()
    {
        if (_shotCountdown.IsPlaying)
            return;

        //Shot();

        _shotCountdown.ResetTime();
        _shotCountdown.Play();
        OnRangeAttackStarted?.Invoke();
    }

    //private void Shot()
    //{
    //    StartCoroutine(ShotRoutine());
    //}


    //private IEnumerator ShotRoutine()
    //{
    //    yield return StartCoroutine(PreShotRoutine());

    //    if (_projectileEngine.CurrentProjectile != null && _isShoted)
    //    {
    //        _projectileEngine.ShootProjectile();
    //    }
    //    else if(!_isShoted)
    //        Destroy(_projectileEngine.CurrentProjectile);
    //}

    public void FinishAttack()
    {
        OnRangeAttackFinished?.Invoke();
    }
}
