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
        if (Globals.HasWinner) return;
        print("Child loses. Monster wins");
        Globals.WinnerType = PlayerType.Monster;
        Globals.HasWinner = true;
        SceneTransitionManager.Instance.ChangeScene("GameOverScene");
    }
    public void ChildWins()
    {
        if (Globals.HasWinner) return;
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Child;
        Globals.HasWinner = true;
        SceneTransitionManager.Instance.ChangeScene("GameOverScene");
    }
}
