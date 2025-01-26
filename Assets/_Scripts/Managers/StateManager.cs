using System;
using DG.DemiLib.Attributes;
using UnityEngine;

[DeScriptExecutionOrder(-1)]
public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public event Action OnRoundCompleted;    
    public State CurrentState { get; private set; }
    public int Round { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        ChangeState(new WaitForInitializationState());
    }

    public void ChangeState(State newState)
    {
        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
    public void NextRound()
    {
        Round++;
        OnRoundCompleted?.Invoke();
    }
}