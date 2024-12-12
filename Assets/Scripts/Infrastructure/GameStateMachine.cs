using ReadyPlayerMe.AvatarCreator;
using ReadyPlayerMe.Samples.AvatarCreatorElements;
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

    [SerializeField] private Button _confirmPhoto;
    [SerializeField] private RoadGenerator _roadGenerator;
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private SpawnPlayer _playerSpawn;

    [SerializeField] private Button _registrateMan;
    [SerializeField] private AvatarFromPhoto _avatarFromPhoto;
    [SerializeField] private Button _toGameButton;
    [SerializeField] private Button _finishGameButton;

    [SerializeField] private GameObject _registrationForm;
    [SerializeField] private GameObject _photoCaptureForm;
    [SerializeField] private GameObject _menuForm;

    [SerializeField] private Camera _UICameraToAvatar;
    [SerializeField] private Camera _UICamera;
    [SerializeField] private GameObject _GamePlayCamera;
    [SerializeField] private SimpleAvatarCreator SimpleAvatarCreator;


    private void Awake()
    {
        SimpleAvatarCreator.OnAvatarLoaded.AddListener(AvatarLoaded);
        _finishGameButton.onClick.AddListener(GameStart);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GameStart();
        }
    }

    public void AvatarLoaded(AvatarProperties avatar)
    {
        GameStart();
    }
    public void GameStart()
    {
        _toGameButton.onClick.AddListener(OnToTheGameButtonClick);
        _registrateMan.onClick.AddListener(OnClickRegistrationButton);
        _avatarFromPhoto.OnAvatarLoaded += OnAvatarLoad;

        _UICameraToAvatar.gameObject.SetActive(false);
        _GamePlayCamera.gameObject.SetActive(false);
        _UICamera.gameObject.SetActive(true);
        _roadGenerator.ResetRoads();

        _registrationForm.SetActive(true);
        _photoCaptureForm.SetActive(false);
        _menuForm.SetActive(false);
        _playerStateMachine.Init();
        _finishGameButton.gameObject.SetActive(false);

        ReadyGame?.Invoke();
    }

    public void DisposeResources()
    {
        _avatarFromPhoto.OnAvatarLoaded -= OnAvatarLoad;
        _registrateMan.onClick.RemoveListener(OnClickRegistrationButton);
        _toGameButton.onClick.RemoveListener(OnToTheGameButtonClick);
    }


    public void GameEnd()
    {
        EndGame?.Invoke();
        DisposeResources();
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
        GameStart();

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


    public void OnClickRegistrationButton()
    {
        _registrationForm.SetActive(false);
        _photoCaptureForm.SetActive(true);
        _UICameraToAvatar.gameObject.SetActive(false);
        _GamePlayCamera.gameObject.SetActive(false);
    }
    public void OnAvatarLoad()
    {
        _photoCaptureForm.SetActive(false);
        _registrationForm.SetActive(false);
        _menuForm.SetActive(true);
        _UICamera.gameObject.SetActive(false);
        _UICameraToAvatar.gameObject.SetActive(true);
        _GamePlayCamera.gameObject.SetActive(false);
    }
    public void OnToTheGameButtonClick() 
    {
        _avatarFromPhoto.SetAvatarToSpawn();
        _UICameraToAvatar.gameObject.SetActive(false);
        _menuForm.SetActive(false);
        _roadGenerator.StartGenerate();
        _playerSpawn.SetupPlayer();
        _GamePlayCamera.gameObject.SetActive(true);
        _finishGameButton.gameObject.SetActive(true);
    }
}
