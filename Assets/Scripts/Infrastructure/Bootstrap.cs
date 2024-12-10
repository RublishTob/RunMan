using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    //System
    [SerializeField] private GameStateMachine _gameStateMachine;
    [SerializeField] private SceneLoader _sceneLoader;

    //Player
    [SerializeField] private Animator _animatorForMaskot;
    [SerializeField] private GameObject _background;
    [SerializeField] private TMP_Text _text;

    //UI
    [SerializeField] private GameObject _winUI;
    [SerializeField] private GameObject _startUI;
    [SerializeField] private GameObject _failUI;
    [SerializeField] private GameObject _fade;
    [SerializeField] private GameObject _restartButton;

    private void Awake()
    {
            ServiceLocator.Instance.RegisterService(_gameStateMachine);
            ServiceLocator.Instance.RegisterService(_sceneLoader);

            InitializeService();
    }
    private void InitializeService()
    {
        //var stateMachine = ServiceLocator.Instance.GetService<GameStateMachine>();
        //stateMachine.Init();
    }
}
