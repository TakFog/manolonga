using UnityEngine;

public class Globals
{
    public static PlayerType WinnerType;
    public static PlayerType PlayerType { get; set; }
    public static Child Child;
    public static Monster Monster;
    public static string ServerAddress = "https://ffa2-109-55-143-128.ngrok-free.app";
    
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
