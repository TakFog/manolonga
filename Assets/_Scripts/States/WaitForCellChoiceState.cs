using System.Collections;
using UnityEngine;

public class WaitForCellChoiceState : State
{
    private Choice choice;
    private int allowedDistance;
    private Vector3 entityPosition;

    public WaitForCellChoiceState(Choice choice, int allowedDistance, Vector3 entityPosition)
    {
        this.choice = choice;
        this.allowedDistance = allowedDistance;
        this.entityPosition = entityPosition;
    }
    
    public override void Enter()
    {
        base.Enter();
        StateManager.Instance.StartCoroutine(C_WaitForCellChoice(choice, allowedDistance, entityPosition));
    }
    IEnumerator C_WaitForCellChoice(Choice choice, int allowedDistance, Vector3 entityPosition)
    {
        //TILEMAPMANAGER convert position and get allowed cells
        StateManager.Instance.ChangeState(new SendChoiceState(choice));
        throw new System.NotImplementedException();
    }
    public override void Exit()
    {
        base.Exit();
    }
}