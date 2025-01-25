public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
        CommunicationManager.Instance.OnMovesReceived += MovesReceived;
    }
    void MovesReceived(CommunicationData communicationOfThisRound)
    {
        StateManager.Instance.ChangeState(new WaitForExecutionState(communicationOfThisRound));
    }
    public override void Exit()
    {
        base.Exit();
        CommunicationManager.Instance.OnMovesReceived -= MovesReceived;
    }
}