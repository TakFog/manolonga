using System.Collections;
using UnityEngine;

public class WaitForExecutionState : State
{
    private CommunicationData communicationsOfThisRound;

    public WaitForExecutionState(CommunicationData communicationsOfThisRound)
    {
        this.communicationsOfThisRound = communicationsOfThisRound;
    }
    public override void Enter()
    {
        base.Enter();
        //Execute
        StateManager.Instance.NextRound();
        StateManager.Instance.ChangeState(new WaitForActionChoiceState());
    }

    IEnumerator C_Execute()
    {
        
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
