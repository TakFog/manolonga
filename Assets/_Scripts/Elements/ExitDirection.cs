using UnityEngine;
using UnityEngine.Serialization;

public class ExitDirection : MonoBehaviour
{
    public GameObject flame;
    
    public GameObject north;
    public GameObject northwest;
    public GameObject west;
    public GameObject southwest;
    public GameObject south;
    public GameObject southeast;
    public GameObject east;
    public GameObject northeast;
    
    public Exit[] exits;

    public void ShowExitDirection()
    {
        var playerPos = Globals.Player.transform.position;
        Vector3 minpos = playerPos;
        float minsqrt = float.MaxValue;
        foreach (var exit in exits)
        {
            if (!exit.opened) continue;
            var sqrt = (playerPos - exit.transform.position).sqrMagnitude;
            if (sqrt < minsqrt)
            {
                minpos = exit.transform.position;
                minsqrt = sqrt;
            }
        }
        var dir = DiscreteDirection.GetOctagonDirection(playerPos, minpos);
        
        north.SetActive(false);
        northwest.SetActive(false);
        west.SetActive(false);
        southwest.SetActive(false);
        south.SetActive(false);
        southeast.SetActive(false);
        east.SetActive(false);
        northeast.SetActive(false);

        switch (DiscreteDirection.ToCardinal(dir))
        {
            case Cardinal.North:
                north.SetActive(true);
                break;
            case Cardinal.East:
                east.SetActive(true);
                break;
            case Cardinal.West:
                west.SetActive(true);
                break;
            case Cardinal.South:
                south.SetActive(true);
                break;
            case Cardinal.NorthEast:
                northwest.SetActive(true);
                break;
            case Cardinal.NorthWest:
                northwest.SetActive(false);
                break;
            case Cardinal.SouthEast:
                southeast.SetActive(false);
                break;
            case Cardinal.SouthWest:
                southwest.SetActive(false);
                break;
        }
        
        gameObject.SetActive(true);
    }
}
