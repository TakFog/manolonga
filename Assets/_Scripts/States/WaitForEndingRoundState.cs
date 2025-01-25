using System.Collections;

public class WaitForEndingRoundState : State
{
    public override void Enter()
    {
        base.Enter();
        StateManager.Instance.StartCoroutine(C_EndRound());
    }

    IEnumerator C_EndRound()
    {
        yield return null;
        CommunicationManager.Instance.ResetRequests();
        StateManager.Instance.NextRound();
        StateManager.Instance.ChangeState(new WaitForActionChoiceState());
    }
    public override void Exit()
    {
        base.Exit();
    }
}