using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaitForCellChoiceState : State
{
    private Choice choice;
    private int allowedDistance;
    private Vector3 entityPosition;

    private bool waitingInput = false;
    private Vector3Int? clickedCell = null;

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
        Vector3Int entityCellPosition = TilemapManager.Instance.GetCellAtWorldPosition(entityPosition);
        List<Vector3Int> walkableCells = TilemapManager.Instance.GetNearCells(entityCellPosition, allowedDistance);
        HighlightManager.Instance.HighlightCells(walkableCells);
        
        waitingInput = true;
        yield return new WaitUntil(() => clickedCell.HasValue && walkableCells.Contains(clickedCell.Value));
        waitingInput = false;
        clickedCell = null;
        HighlightManager.Instance.UnhighlightCells(walkableCells);
        
        choice.EndCell = clickedCell.Value;
        StateManager.Instance.ChangeState(new SendChoiceState(choice));
    }
    public override void Exit()
    {
        base.Exit();
    }
}