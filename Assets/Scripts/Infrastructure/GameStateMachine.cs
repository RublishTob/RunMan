using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameStateMachine : MonoBehaviour
{
    public event Action StartGame;
    public event Action EndGame;
    public event Action WinGame;
    public event Action ReadyGame;

    [SerializeField] private Button _startbutton;
    [SerializeField] private Button _restartbutton;

    private SceneLoader _sceneLoader;


    private void OnDisable()
    {
        _startbutton.onClick.RemoveListener(GameStart);
        _restartbutton.onClick.RemoveListener(GameReady);
    }
    public void Init()
    {
        _startbutton.onClick.AddListener(GameStart);
        _restartbutton.onClick.AddListener(GameReady);
        _sceneLoader = ServiceLocator.Instance.GetService<SceneLoader>();
        GameReady();
    }
    public void GameStart()
    {
        StartGame?.Invoke();
    }
    public void GameReady()
    {
        ReadyGame?.Invoke();
    }
    public void GameEnd()
    {
        EndGame?.Invoke();
        StartCoroutine(ShowFailPanelAndRestart());
    }
    public void GameWin()
    {
        WinGame?.Invoke();
        StartCoroutine(ShowWaterAndRestart());

    }
    private IEnumerator ShowWaterAndRestart()
    {
        yield return StartCoroutine(FillWater());
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(GameReadyStart());
    }
    private IEnumerator GameReadyStart()
    {
        yield return new WaitForSeconds(0.5f);
        GameReady();

    }
    private IEnumerator FillWater()
    {
        yield return null;
    }
    private IEnumerator ShowFailPanelAndRestart()
    {
        yield return StartCoroutine(FailPanel());
        yield return StartCoroutine(GameReadyStart());
    }
    private IEnumerator FailPanel()
    {
        yield return new WaitForSeconds(2f);
    }

}
