public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
        UIManager.Instance.SetWaitEnabled(true);
        CommunicationManager.Instance.OnMovesReceived += MovesReceived;
    }
    void MovesReceived(CommunicationData communicationOfThisRound)
    {
        StateManager.Instance.ChangeState(new WaitForExecutionState(communicationOfThisRound));
    }
    public override void Exit()
    {
        base.Exit();
        UIManager.Instance.SetWaitEnabled(false);
        CommunicationManager.Instance.OnMovesReceived -= MovesReceived;
    }
}