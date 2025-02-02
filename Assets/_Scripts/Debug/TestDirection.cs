using UnityEngine;

public class TestDirection : MonoBehaviour
{
    public GameObject source;
    public GameObject destination;
    public GameObject direction;
    
    public bool hexagon;

    public Cardinal cardinal;

    // Update is called once per frame
    void Update()
    {
        Vector2 dir;
        if (hexagon)
            dir = DiscreteDirection.GetHexagonDirection(source.transform.position, 
                destination.transform.position, source.transform.localScale);
        else
            dir = DiscreteDirection.GetOctagonDirection(source.transform.position, 
                destination.transform.position);
        direction.transform.position = source.transform.position + (Vector3)dir;
        cardinal = DiscreteDirection.ToCardinal(dir);
    }
}
