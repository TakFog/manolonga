using UnityEngine;

public class Globals
{
    public static PlayerType playerType;
    public static Child child;
    public static Monster monster;

    public static GameObject Player
    {
        get
        {
            if (playerType == PlayerType.Child)
            {
                return child.gameObject;
            }
            else
            {
                return monster.gameObject;
            }
        }
    }
}
