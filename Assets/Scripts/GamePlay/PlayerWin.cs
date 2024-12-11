
using ReadyPlayerMe.Samples.QuickStart;

public class PlayerWin : PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    public PlayerWin(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input) : base(stateMachine, movement, input)
    {
        _stateMachine = stateMachine;
        _movement = movement;
    }

    public override void Exit()
    {
    }

    public override void Start()
    {
    }

    public override void Update()
    {
    }
}
