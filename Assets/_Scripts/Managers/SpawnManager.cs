using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MyBehaviour
{
    public static SpawnManager ChildInstance { get; private set; }
    public static SpawnManager MonsterInstance { get; private set; }

    public PlayerType player;

    public int SpawnPointCount { get { return transform.childCount; } }

    private void Awake()
    {
        if (player == PlayerType.Monster)
        {
            MonsterInstance = this;
        }
        else
        {
            ChildInstance = this;
        }
    }

    protected override void OnEnableAfterStart()
    {
        Debug.Log("registering spawn for " + player);
        if (player == PlayerType.Monster)
            CommunicationManager.Instance.OnMonsterSpawnReceived += SelectSpawnPoint;
        else
            CommunicationManager.Instance.OnChildSpawnReceived += SelectSpawnPoint;
    }

    void OnDisable()
    {
        if (player == PlayerType.Monster)
            CommunicationManager.Instance.OnMonsterSpawnReceived -= SelectSpawnPoint;
        else
            CommunicationManager.Instance.OnChildSpawnReceived -= SelectSpawnPoint;
    }

    void SelectSpawnPoint(int index)
    {
        Debug.Log("Spawn " + player + " @ " + index);
        var pos = transform.GetChild(index).position;
        pos = TilemapManager.Instance.AlignToCell(pos);
        if (player == PlayerType.Monster)
            Globals.Monster.transform.position = pos;
        else
            Globals.Child.transform.position = pos;
    }


}
