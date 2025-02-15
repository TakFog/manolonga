using System.Collections;
using UnityEngine;

public class Globals
{
    public static bool HasWinner = false;
    public static PlayerType WinnerType;
    public static PlayerType PlayerType { get; set; }
    public static Child Child;
    public static Monster Monster;
#if UNITY_EDITOR
    public static string DefaultServerAddress = "http://localhost:8080";
    public static string ServerAddress = DefaultServerAddress + "/DEBUG";
#else
    public static string DefaultServerAddress = "https://manolonga.vercel.app/";
    public static string ServerAddress = "";
#endif

    public static GameObject Player
    {
        get
        {
            if (PlayerType == PlayerType.Child)
            {
                return Child.gameObject;
            }
            else
            {
                return Monster.gameObject;
            }
        }
    }
}
