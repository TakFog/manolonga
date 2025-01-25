using System.Collections;

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

public class WaitForInitializationState : State
{
    public override void Enter()
    {
        base.Enter();
        StateManager.Instance.StartCoroutine(C_WaitForInitialization());
    }

    IEnumerator C_WaitForInitialization()
    {
        yield return CommunicationManager.Instance.StartCoroutine(CommunicationManager.Instance.C_ClearServer());
        StateManager.Instance.ChangeState(new WaitForActionChoiceState());
    }

    public override void Exit()
    {
        base.Exit();
    }
}