using System;
using UnityEngine;

public class TileGameObject : MonoBehaviour
{
    public bool IsWalkableForChild = true;
    public bool IsWalkableForMonster = true;

    private void Start()
    {
        TilemapManager.Instance.Tiles.Add(TilemapManager.Instance.GetCellAtWorldPosition(transform.position), this);
    }
}
