
using ReadyPlayerMe.Samples.QuickStart;

public class PlayerLoose : PlayerState
{
    private PlayerStateMachine _stateMachine;
    private ThirdPersonMovement _movement;
    public PlayerLoose(PlayerStateMachine stateMachine, ThirdPersonMovement movement, PlayerInput input) : base(stateMachine, movement, input)
    {
        _stateMachine = stateMachine;
        _movement = movement;
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
