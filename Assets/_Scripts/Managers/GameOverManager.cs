using UnityEngine;
using UnityEngine.Events;

public class GameOverManager : MonoBehaviour
{
    public UnityEvent OnGameOver;
    public static GameOverManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void MonsterWins()
    {
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Monster;
        OnGameOver?.Invoke();
    }
    public void ChildWins()
    {
        print("Child wins. Monster loses");
        Globals.WinnerType = PlayerType.Child;
        OnGameOver?.Invoke();
    }
}
