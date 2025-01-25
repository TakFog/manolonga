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
        CommunicationManager.Instance.SubmitChoice(choice, Globals.PlayerType, StateManager.Instance.Round);
        StateManager.Instance.ChangeState(new WaitForOpponentChoiceReceivedState());
    }
    public override void Exit()
    {
        base.Exit();
    }
}