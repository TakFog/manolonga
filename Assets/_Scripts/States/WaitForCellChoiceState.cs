using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        CellClickManager.Instance.OnCellClicked += RegisterClick;
        StateManager.Instance.StartCoroutine(C_WaitForCellChoice(choice, allowedDistance, entityPosition));
    }
    void RegisterClick(Vector3Int cellPosition)
    {
        if(waitingInput)
            clickedCell = cellPosition;
    }
    IEnumerator C_WaitForCellChoice(Choice choice, int allowedDistance, Vector3 entityPosition)
    {
        Vector3Int entityCellPosition = TilemapManager.Instance.GetCellAtWorldPosition(entityPosition);
        List<CellPath> cellPaths = TilemapManager.Instance.GetCellPaths(entityCellPosition, allowedDistance);
        List<Vector3Int> walkableCells = cellPaths.Select(x => x.cell).ToList();
        HighlightManager.Instance.HighlightCells(walkableCells);
        waitingInput = true;
        yield return new WaitUntil(() => clickedCell.HasValue && walkableCells.Contains(clickedCell.Value));
        HighlightManager.Instance.UnhighlightCells(walkableCells);
        choice.PositionsPath = cellPaths[walkableCells.IndexOf(clickedCell.Value)].worldPath;
        clickedCell = null;      
        waitingInput = false;
        StateManager.Instance.ChangeState(new SendChoiceState(choice));
    }
    public override void Exit()
    {
        base.Exit();
        CellClickManager.Instance.OnCellClicked -= RegisterClick;
    }
}