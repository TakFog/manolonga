using System;
using System.Collections;
using System.Collections.Generic;
using IngameDebugConsole;
using UnityEngine;

public class Child : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private int walkTiles;
    [SerializeField, Range(1, 10)] private int runTiles;

    private void Start()
    {
        //DebugLogConsole.
    }

    public IEnumerator ChooseAction(EntityAction action)
    {
        switch (action)
        {
            case EntityAction.Walk:
                
                break;
            case EntityAction.Run:
                break;
            case EntityAction.CheckWind:
                break;
        }
        SendAction();
        return null;
    }

    public void SendAction()
    {
        print("sending action");
    }
    public void ExecuteAction(EntityAction action)
    {
        switch (action)
        {
            case EntityAction.Walk:
                ExecuteWalk(new Vector3Int(0, 0, 0));
                break;
            case EntityAction.Run:
                ExecuteRun(new Vector3Int(0, 0, 0));
                break;
            case EntityAction.CheckWind:
                ExecuteCheckWind();
                break;
        }
    }
    public void ExecuteWalk(Vector3Int tilePosition)
    {
        Vector3 tileWorldPosition = TilemapManager.Instance.GetCellWorldCenterPosition(tilePosition);
        transform.Translate(tileWorldPosition - transform.position);
        print("the child walked to the tile at" + tileWorldPosition);
    }
    public void ExecuteRun(Vector3Int tilePosition)
    {
        Vector3 tileWorldPosition = TilemapManager.Instance.GetCellWorldCenterPosition(tilePosition);
        transform.Translate(tileWorldPosition - transform.position);
        print("the child ran to the tile at" + tileWorldPosition);
    }
    public void ExecuteCheckWind()
    {
        print("the child checked the wind");
    }
}

public enum EntityAction
{
    Walk,
    Run,
    CheckWind,
}

public class Childd : MonoBehaviour
{
    public void ChooseAction(EntityAction action)
    {
        if (StateManager.Instance.CurrentState.GetType() != typeof(WaitForActionChoiceState))
            return;

        switch (action)
        {
            case EntityAction.Walk:
                StateManager.Instance.ChangeState(new WaitForMovementActionTileChoiceState());
                break;
            case EntityAction.Run:
                StateManager.Instance.ChangeState(new WaitForMovementActionTileChoiceState());
                break;
            case EntityAction.CheckWind:
                StateManager.Instance.ChangeState(new SendChoiceState());
                break;
        }
    }
}

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public event Action<State> OnStateChanged;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        //DebugLogConsole.AddCommandInstance("cs", "Change state", "ChangeState", this, "state");
    }

    private void Start()
    {
        ChangeState(new WaitForActionChoiceState());
    }

    public State CurrentState { get; private set; }
    public void ChangeState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();

        OnStateChanged?.Invoke(CurrentState);
    }

    private void Update()
    {
        CurrentState.Update();
    }
}
public abstract class State
{
    public virtual void Enter()
    {
        Debug.Log("Enter " + GetType().Name);
    }
    public virtual void Update() { }
    public virtual void Exit()
    {
        Debug.Log("Exit " + GetType().Name);

    }
}
public class WaitForActionChoiceState : State
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class WaitForMovementActionTileChoiceState : State
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class SendChoiceState : State
{
    public SendChoiceState()
    {
        
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class WaitForOpponentChoiceReceivedState : State
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
public class WaitForActionExecutionState : State
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}

[Serializable]
public class Choice
{
    public int round;
    public EntityAction actionType;
    public Vector3Int endCell;
}