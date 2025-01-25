using UnityEngine;

public class TestAudio : MonoBehaviour
{
    public AudioManager audioManager;
    public Child child;
    public Monster monster;

    [ContextMenu("Play")]
    public void UpdateDistance()
    {
        Globals.Child = child;
        Globals.Monster = monster;
        audioManager.UpdateDistance();
    }
}
