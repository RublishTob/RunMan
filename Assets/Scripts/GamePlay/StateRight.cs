
using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class StateRight : PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    private PlayerInput _playerInput;
    public StateRight(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input) : base(stateMachine, movement, input)
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
        _movement.MoveRight(1);
    }

    public override void Update()
    {
        _playerInput.CheckInput();
        var xAxisInput = _playerInput.AxisHorizontal;

        if (xAxisInput == 0)
        {
            _movement.MoveDefault();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            _stateMachine.SwichState<StateMidle>();
        }
    }
}
