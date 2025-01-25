using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }
    public event Action<State> OnStateChanged;

    public int Round { get; private set; } = 0;

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