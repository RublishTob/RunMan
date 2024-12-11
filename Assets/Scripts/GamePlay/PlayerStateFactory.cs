using ReadyPlayerMe.Samples.QuickStart;
using System;
using UnityEngine;

public class PlayerStateFactory : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine _stateMashine;
    private GameStateMachine _gameStateMachine;
    [SerializeField] private ThirdPersonMovement _movement;
    [SerializeField] private PlayerInput _input;

    public void Init()
    {
        //_stateMashine = ServiceLocator.Instance.GetService<MascotStateMachine>();
        //_mascotView = ServiceLocator.Instance.GetService<MascotView>();
        //_gameStateMachine = ServiceLocator.Instance.GetService<GameStateMachine>();
        //_uiWindowsManager = ServiceLocator.Instance.GetService<UIWindowsManager>();
    }
    public PlayerState Create(string stateName)
    {
        if (_stateMashine == null)
        {
            throw new NullReferenceException("State Machine is null");
        }
        if (_movement == null)
        {
            throw new NullReferenceException("ThirdPersonController is null");
        }
        PlayerState gameState = MakeState(stateName);

        return gameState;
    }
    private PlayerState MakeState(string stateName)
    {
        switch (stateName)
        {
            case "StateMidle":
                return new StateMidle(_stateMashine, _movement, _input);
            case "StateRight":
                return new StateRight(_stateMashine, _movement, _input);
            case "StateLeft":
                return new StateLeft(_stateMashine, _movement, _input);
            case "PlayerDeath":
                return new PlayerDeath(_stateMashine, _movement, _input);
            case "PlayerWin":
                return new PlayerWin(_stateMashine, _movement, _input);
            case "PlayerLoose":
                return new PlayerLoose(_stateMashine, _movement, _input);
        }
        return null;

    }
}