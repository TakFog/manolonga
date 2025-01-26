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
        SceneTransitionManager.Instance.ChangeScene("GameOverScene");
    }
    public void ChildWins()
    {
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Child;
        SceneTransitionManager.Instance.ChangeScene("GameOverScene");
    }
}
