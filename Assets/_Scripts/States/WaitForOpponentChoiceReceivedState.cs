public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
        //Subscribe to moves received event
    }
    void MovesReceived()
    {
        StateManager.Instance.ChangeState(new WaitForExecutionState());
    }
    public override void Exit()
    {
        base.Exit();
    }
}