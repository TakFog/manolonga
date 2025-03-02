using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private Sprite exitClosed;

    public void SetClosed()
    {
        GetComponent<SpriteRenderer>().sprite = exitClosed;
    }

    public void CloseExit()
    {
        SetClosed();
        ExitManager.Instance.CloseExit(this);
    }
}