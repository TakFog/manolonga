using IngameDebugConsole;
using UnityEngine;

public class TESTCommunicationManager : MonoBehaviour
{
    public static TESTCommunicationManager Instance;
    public PlayerType player;
    public Choice choice; // TODO Remove

    private void Awake()
    {
        Instance = this;
    }
    
    [ConsoleMethod("send", "Send a fake choice")]
    public static void SendFakeChoice()
    {
        CommunicationManager.Instance.SubmitChoice(Instance.choice, Instance.player, StateManager.Instance.Round);
        print("Fake choice sent");
    }
}