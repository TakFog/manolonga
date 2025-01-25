using UnityEngine;

public class Exit : MonoBehaviour
{
    public void CloseExit()
    {
        ExitManager.Instance.CloseExit(this);
    }
}