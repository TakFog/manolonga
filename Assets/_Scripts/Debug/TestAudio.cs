using UnityEngine;
using UnityEngine.Serialization;

public class TestAudio : MonoBehaviour
{
    [FormerlySerializedAs("audioManager")] public AudioEntity audioEntity;
    public Child child;
    public Monster monster;

    [ContextMenu("Play")]
    public void UpdateDistance()
    {
        Globals.Child = child;
        Globals.Monster = monster;
        audioEntity.UpdateDistance();
    }
}
