using System.Collections;
using NUnit.Framework;
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
        StateManager.Instance.StartCoroutine(C_Execute());
    }

    IEnumerator C_Execute()
    {
        Child child = Globals.Child;
        Monster monster = Globals.Monster;
        child.StartCoroutine(child.C_ExecuteChoice(communicationsOfThisRound.Child));
        monster.StartCoroutine(monster.C_ExecuteChoice(communicationsOfThisRound.Monster));
        yield return new WaitWhile(()=> child.IsExecuting || monster.IsExecuting);
        StateManager.Instance.ChangeState(new WaitForEndingRoundState());
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}