using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTile : MonoBehaviour
{
    public GameObject go;
    public Vector3Int cell;
    public int distance;
    public Transform parent;

    [ContextMenu("WorldToTile")]
    public void WorldToTile()
    {
        var pos = TilemapManager.Instance.GetCellAtWorldPosition(go.transform.position);
        Debug.Log(pos);
    }

    [ContextMenu("Near")]
    public void Near()
    {
        foreach (var point in TilemapManager.Instance.GetNearCellsWorldPoints(cell, distance))
        {
            Instantiate(go, point, Quaternion.identity, parent);
        }
    }
}
