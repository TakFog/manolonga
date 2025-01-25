using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
    }
    void MovesReceived(CommunicationData communicationData)
    {
        StateManager.Instance.ChangeState(new WaitForExecutionState());
    }
    public override void Exit()
    {
        base.Exit();
    }
}