using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerType playerType;
    Child child;
    Monster monster;

    private void Awake()
    {
        child = GetComponent<Child>();
        monster = GetComponent<Monster>();
    }

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

