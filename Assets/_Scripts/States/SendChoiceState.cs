public class SendChoiceState : State
{
    Choice choice;
    public SendChoiceState(Choice choice)
    {
        this.choice = choice;
    }
    public override void Enter()
    {
        base.Enter();
        //SENDREQUEST(choice)
        StateManager.Instance.ChangeState(new WaitForOpponentChoiceReceivedState());
    }
    public override void Exit()
    {
        base.Exit();
    }
}