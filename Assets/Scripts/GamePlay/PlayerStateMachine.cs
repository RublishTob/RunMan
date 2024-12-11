using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerState _currentState;
    [SerializeField] private PlayerStateFactory _stateMascotFactory;
    private Dictionary<Type, PlayerState> _states;

    public void Init()
    {
        //_stateMascotFactory = ServiceLocator.Instance.GetService<PlayerStateFactory>();
        _states = new Dictionary<Type, PlayerState>()
        {
            [typeof(StateMidle)] = _stateMascotFactory.Create(typeof(StateMidle).ToString()),
            [typeof(StateRight)] = _stateMascotFactory.Create(typeof(StateRight).ToString()),
            [typeof(StateLeft)] = _stateMascotFactory.Create(typeof(StateLeft).ToString()),
            [typeof(PlayerWin)] = _stateMascotFactory.Create(typeof(PlayerWin).ToString()),
            [typeof(PlayerLoose)] = _stateMascotFactory.Create(typeof(PlayerLoose).ToString()),
            [typeof(PlayerDeath)] = _stateMascotFactory.Create(typeof(PlayerDeath).ToString())
        };

        StartState<StateMidle>();
    }

    public void SwichState<T>() where T : PlayerState
    {
        _currentState.Exit();

        StartState<T>();
    }
    private void StartState<T>() where T : PlayerState
    {
        _currentState = _states[typeof(T)];
        _currentState.Start();
    }

    private void Update()
    {
        if (_currentState == null)
        {
            return;
        }
        Debug.Log ($"{_currentState}");
        _currentState.Update();
    }
}
