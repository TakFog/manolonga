using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MonsterWins()
    {
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Monster;
        //ChangeScene
    }
    public void ChildWins()
    {
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Child;
        //ChangeScene
    }
}
