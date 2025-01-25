using System.Collections.Generic;
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
    public List<Vector3> GetCellWorldCenterPosition(List<Vector3Int> cells)
    {
        var points = new List<Vector3>();
        foreach (var cell in cells)
        {
            points.Add(GetCellWorldCenterPosition(cell));
        }
        return points;
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

    public List<CellPath> GetCellPaths(Vector3Int cellPosition, int distance)
    {
        //mock w/o paths
        var cells = GetNearCells(cellPosition, distance);
        var paths = new List<CellPath>();
        foreach (var cell in GetNearCells(cellPosition, distance))
        {
            paths.Add(new CellPath(cell, GetCellWorldCenterPosition(cell)));
        }
        return paths;
    }

    public List<Vector3Int> GetNearCells(Vector3Int cellPosition, int distance)
    {
        if (distance < 1) return new List<Vector3Int>();
        if (distance == 1)
        {
            var result = new List<Vector3Int>();
            AddIfWalkable(result, cellPosition, -1, 0);
            AddIfWalkable(result, cellPosition, 1, 0);
            if (cellPosition.y % 2 == 0)
            {
                AddIfWalkable(result, cellPosition, -1, 1);
                AddIfWalkable(result, cellPosition, -1, -1);
                AddIfWalkable(result, cellPosition, 0, 1);
                AddIfWalkable(result, cellPosition, 0, -1);
            }
            else
            {
                AddIfWalkable(result, cellPosition, 0, 1);
                AddIfWalkable(result, cellPosition, 0, -1);
                AddIfWalkable(result, cellPosition, 1, 1);
                AddIfWalkable(result, cellPosition, 1, -1);
            }
            return result;
        }
        
        HashSet<Vector3Int> added = new HashSet<Vector3Int>();
        List<Vector3Int> fringe = new List<Vector3Int>();
        fringe.Add(cellPosition);
        added.Add(cellPosition);
        for (int i = 0; i < distance; i++)
        {
            var next = new List<Vector3Int>();
            foreach (var cell in fringe)
            {
                foreach (var near in GetNearCells(cell, 1))
                {
                    if (!added.Contains(near))
                    {
                        next.Add(near);
                        added.Add(near);
                    }
                } 
            }

            fringe = next;
        }

        added.Remove(cellPosition);
        return new List<Vector3Int>(added);
    }

    public List<Vector3> GetNearCellsWorldPoints(Vector3Int cellPosition, int distance)
    {
        return GetCellWorldCenterPosition(GetNearCells(cellPosition, distance));
    }

    private void AddIfWalkable(List<Vector3Int> list, Vector3Int cellPosition, int deltax, int deltay)
    {
        var cell = new Vector3Int(cellPosition.x + deltax, cellPosition.y + deltay, cellPosition.z);
        if (IsCellWalkable(cell))
            list.Add(cell);
    }
}
