using UnityEngine;

public class TileGameObject : MonoBehaviour
{
    public bool IsWalkableForChild;
    public bool IsWalkableForMonster;

    private void Start()
    {
        TilemapManager.Instance.Tiles.Add(TilemapManager.Instance.GetCellAtWorldPosition(transform.position), this);
    }
}