using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public abstract class PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    private PlayerInput _input;
    public PlayerState(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input)
    {
        _stateMachine = stateMachine;
        _movement = movement;
        _input = input;
    }

    public abstract void Start();

    public abstract void Update();

    public abstract void Exit();
}
