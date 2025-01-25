using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public event Action OnRoundCompleted;    
    public State CurrentState { get; private set; }
    public int Round { get; private set; } = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        ChangeState(new WaitForActionChoiceState());
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
    }
    private void Update()
    {
        CurrentState?.Update();
    }
}