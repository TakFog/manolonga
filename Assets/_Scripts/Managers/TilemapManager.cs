using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance;
    
    private Tilemap tilemap;
    
    private void Awake()
    {
        Instance = this;
        if (tilemap == null)
            tilemap = FindAnyObjectByType<Tilemap>();
    }
    
    public Vector3 GetCellWorldCenterPosition(Vector3Int cellPosition)
    {
        return tilemap.GetCellCenterWorld(cellPosition);
    }
    public Vector3Int GetCellAtWorldPosition(Vector3 worldPosition)
    {
        return tilemap.WorldToCell(worldPosition);
    }
    public bool IsCellWalkable(Vector3Int cellPosition)
    {
        //TBD
        return true;
    }
}
