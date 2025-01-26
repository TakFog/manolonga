using UnityEngine;

public class EntityZFix : MonoBehaviour
{
    SpriteRenderer sr;
    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        sr.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
    }
}
