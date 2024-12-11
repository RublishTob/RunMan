
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class StateMidle : PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    private PlayerInput _playerInput;
    public StateMidle(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input) : base(stateMachine, movement, input)
    {
        _stateMachine = stateMachine;
        _movement = movement;
        _playerInput = input;
    }

    public override void Exit()
    {
    }

    public override void Start()
    {
        if (_playerInput.AxisHorizontal > 0)
        {
            _movement.MoveMidle(1);
        }
        else if (_playerInput.AxisHorizontal < 0)
        {
            _movement.MoveMidle(-1);
        }
    }

    public override void Update()
    {
        _playerInput.CheckInput();

        var xAxisInput = _playerInput.AxisHorizontal;

        if (xAxisInput == 0)
        {
            _movement.MoveDefault();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _stateMachine.SwichState<StateRight>();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _stateMachine.SwichState<StateLeft>();
        }
    }
}
