using UnityEngine;

public class Globals
{
    public static PlayerType WinnerType;
    public static PlayerType PlayerType { get; set; }
    public static Child Child;
    public static Monster Monster;
    public static string ServerAddress = "";
    
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
