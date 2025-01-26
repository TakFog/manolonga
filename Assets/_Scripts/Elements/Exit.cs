using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Sprite exitClosed;
    public void CloseExit()
    {
        GetComponent<SpriteRenderer>().sprite = exitClosed;
        ExitManager.Instance.CloseExit(this);
    }
}