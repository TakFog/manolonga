using System;
using UnityEngine;
using UnityEngine.Serialization;

public class TestAudio : MonoBehaviour
{
    [FormerlySerializedAs("audioManager")] public AudioEntity audioEntity;
    public Child child;
    public Monster monster;
    public bool onUpdate;

    [ContextMenu("Play")]
    public void UpdateDistance()
    {
        Globals.Child = child;
        Globals.Monster = monster;
        audioEntity.UpdateDistance();
    }

    private void Update()
    {
        if (onUpdate)
            UpdateDistance();
    }
}
