using UnityEngine;
using Elementary;
using System;
using System.Collections;

public class RangeAttackEngine : MonoBehaviour
{
    public event Action OnRangeAttackStarted;

    public event Action OnRangeAttackFinished;

    public TimerBehaviour ShotFullCountdown => _shotFullCountdown;

    //public bool IsShoted => _isShoted;

    //[SerializeField]
    //private EventReceiver _rangeAttackReciever; 

    //[SerializeField]
    //private ProjectileEngine _projectileEngine;

    [Header("Shot Full Countdown")]
    [SerializeField]
    private TimerBehaviour _shotFullCountdown;

    [Header("Pre-Shot Countdown")]
    [SerializeField]
    private float _preshotCountdown;
        
    //private bool _isShoted;

    //private Coroutine _shotRoutine;


    private void OnEnable()
    {
        //_rangeAttackReciever.OnEvent += OnRequestRangeAttack;
        _shotFullCountdown.OnFinished += FinishAttack;
    }

    private void OnDisable()
    {
       // _rangeAttackReciever.OnEvent -= OnRequestRangeAttack;
        _shotFullCountdown.OnFinished -= FinishAttack;
    }
    public void TryShoot()
    {
        if (_shotFullCountdown.IsPlaying)
            return;

        //Shot();
        //OnRangeAttackFinished?.Invoke();

        _shotFullCountdown.ResetTime();
        _shotFullCountdown.Play();
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
