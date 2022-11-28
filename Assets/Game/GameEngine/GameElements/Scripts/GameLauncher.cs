using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class GameLauncher : MonoBehaviour,
    IConstructListener

{
    private GameContext _gameContext;

    [Header("Start Game Timer")]
    [SerializeField]
    private float _delay;

    [SerializeField]
    private float _countdown;

    private float _startDelay;

    private void Awake()
    {
        _startDelay = _delay;
    }

    public void Construct(GameContext context)
    {
        _gameContext = context;
    }

    [Button]
    private void StartGameTimer()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        while (_delay > 0)
        {
            Debug.Log($"Start in {_delay} second!");
            _delay--;
            yield return new WaitForSeconds(_countdown);
        }

        _gameContext.StartGame();

        _delay = _startDelay;
    }    
}
