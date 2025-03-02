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
        var comMan = CommunicationManager.Instance;
        yield return comMan.StartCoroutine(comMan.C_ClearServer());
        var initInput = new InitInputData();
        initInput.numExits = ExitManager.Instance.ActiveExits.Count;
        initInput.numOpenExits = ExitManager.Instance.numberOfUsedExits;
        initInput.numChildSpawns = 1;
        initInput.numMonsterSpawns = 1;
        yield return comMan.StartCoroutine(comMan.C_InitGame(initInput));
        StateManager.Instance.ChangeState(new WaitForActionChoiceState());
    }

    public override void Exit()
    {
        base.Exit();
    }
}