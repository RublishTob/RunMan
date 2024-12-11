using ReadyPlayerMe.Samples.QuickStart;
using UnityEngine;

public class StateLeft : PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    private PlayerInput _playerInput;

    public StateLeft(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input) : base(stateMachine, movement, input)
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
        _movement.MoveLeft(-1);
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
            _stateMachine.SwichState<StateMidle>();
        }
    }
}
