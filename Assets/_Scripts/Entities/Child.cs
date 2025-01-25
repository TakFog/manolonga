using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    
}
public class Child : Entity
{
    [SerializeField, Range(1, 10)] private int walkDistance;
    [SerializeField, Range(1, 10)] private int runDistance;

    private void OnEnable()
    {
        throw new NotImplementedException();
    }
    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    public void ChooseAction(EntityAction action)
    {
        if (StateManager.Instance.CurrentState.GetType() != typeof(WaitForActionChoiceState))
            return;
        
        Choice choice = new Choice();
        choice.ActionType = action;

        switch (action)
        {
            case EntityAction.Walk:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, walkDistance, transform.position));
                break;
            case EntityAction.Run:
                StateManager.Instance.ChangeState(new WaitForCellChoiceState(choice, runDistance, transform.position));
                break;
            case EntityAction.CheckWind:
                StateManager.Instance.ChangeState(new SendChoiceState(choice));
                break;
        }
    }
    public void MovesReceived(MoveData moves)
    {
        ExecuteChoice(moves.Child);
    }
    public void ExecuteChoice(Choice choice)
    {
        switch (choice.ActionType)
        {
            case EntityAction.Walk:
                ExecuteWalk(choice.EndCell);
                break;
            case EntityAction.Run:
                ExecuteRun(choice.EndCell);
                break;
            case EntityAction.CheckWind:
                ExecuteCheckWind();
                break;
        }
    }
    public void ExecuteWalk(Vector3Int tilePosition)
    {
        
    }
    public void ExecuteRun(Vector3Int tilePosition)
    {
        
    }
    public void ExecuteCheckWind()
    {
        
    }
}
public class Monster : Entity

public class Player : MonoBehaviour