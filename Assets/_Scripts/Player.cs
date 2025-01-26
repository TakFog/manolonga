using System;
using IngameDebugConsole;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerType playerType;
    Child child;
    Monster monster;

    private void Start()
    {
        playerType = Globals.PlayerType;
        child = Globals.Child;
        monster = Globals.Monster;
        
        DebugLogConsole.AddCommandInstance("choose", "Choose action", "ChooseAction", this);
    }

    public void ChooseActionWalk() => ChooseAction(EntityActionType.Walk);
    public void ChooseActionRun() => ChooseAction(EntityActionType.Run);
    public void ChooseActionWind() => ChooseAction(EntityActionType.CheckWind);
    public void ChooseActionMove() => ChooseAction(EntityActionType.Move);
    public void ChooseActionAttack() => ChooseAction(EntityActionType.Attack);

    public void ChooseAction(EntityActionType actionType)
    {
        Choice choice = new Choice();
        choice.actionType = actionType;

        switch (actionType)
        {
            case EntityActionType.Walk:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, child.WalkDistance, child.transform.position));
                break;
            case EntityActionType.Run:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, child.RunDistance, child.transform.position));
                break;
            case EntityActionType.CheckWind:
                StateManager.Instance.ChangeState(new SendChoiceState(choice));
                break;
            
            case EntityActionType.Move:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, monster.MoveDistance, monster.transform.position));
                break;
            case EntityActionType.Attack:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, monster.AttackDistance, monster.transform.position));
                break;
        }
    }
}

