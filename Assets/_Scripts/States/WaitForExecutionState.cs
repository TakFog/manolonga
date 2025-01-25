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
        child.StartCoroutine(child.C_ExecuteChoice(communicationsOfThisRound.ChildChoice));
        monster.StartCoroutine(monster.C_ExecuteChoice(communicationsOfThisRound.MonsterChoice));
        yield return new WaitUntil()
        StateManager.Instance.NextRound();
        StateManager.Instance.ChangeState(new WaitForActionChoiceState());
    }
    
    public override void Exit()
    {
        base.Exit();
    }
}
