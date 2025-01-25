using UnityEngine;

public class Globals
{
    public static PlayerType PlayerType;
    public static Child Child;
    public static Monster Monster;

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
