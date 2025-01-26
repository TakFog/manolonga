using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : MonoBehaviour
{
    public static TilemapManager Instance;
    private Tilemap tilemap;
    
    public Dictionary<Vector3Int, TileGameObject> Tiles = new Dictionary<Vector3Int, TileGameObject>();
    
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
        if (!Tiles.ContainsKey(cellPosition))
            return false;
        if (Globals.PlayerType == PlayerType.Child)
            return Tiles[cellPosition].IsWalkableForChild;
        else
        {
            return Tiles[cellPosition].IsWalkableForMonster;
        }
    }

    public List<CellPath> GetCellPaths(Vector3Int cellPosition, int distance)
    {
        if (distance < 1) return new List<CellPath>();
        var result = new List<CellPath>();
        if (distance == 1)
        {
            foreach (var cell in GetNearCells(cellPosition))
            {
                result.Add(new CellPath(cell, GetCellAtWorldPosition(cellPosition)));
            }
            return result;
        }
        
        HashSet<Vector3Int> added = new HashSet<Vector3Int>();
        List<CellPath> fringe = new List<CellPath>();
        fringe.Add(new CellPath(cellPosition, new List<Vector3Int>()));
        added.Add(cellPosition);
        for (int i = 0; i < distance; i++)
        {
            var next = new List<CellPath>();
            foreach (var cell in fringe)
            {
                foreach (var near in GetNearCells(cell.cell, 1))
                {
                    if (!added.Contains(near))
                    {
                        added.Add(near);
                        var nextPath = new List<Vector3Int>();
                        nextPath.AddRange(cell.path);
                        nextPath.Add(near);
                        var cellPath = new CellPath(near, nextPath);
                        next.Add(cellPath);
                        result.Add(cellPath);
                    }
                } 
            }

            fringe = next;
        }

        foreach (var cellPath in result)
        {
            cellPath.worldPosition = GetCellWorldCenterPosition(cellPath.cell);
            cellPath.worldPath = GetCellWorldCenterPosition(cellPath.path);
        }
        return result;
    }

    public List<Vector3Int> GetNearCells(Vector3Int cellPosition)
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
    
    public List<Vector3Int> GetNearCells(Vector3Int cellPosition, int distance)
    {
        if (distance < 1) return new List<Vector3Int>();
        if (distance == 1)
        {
            return GetNearCells(cellPosition);
        }
        
        var result = new List<Vector3Int>();
        foreach (var cellPath in GetCellPaths(cellPosition, distance))
        {
            result.Add(cellPath.cell);
        }
        return result;
    }

    public List<Vector3> GetNearCellsWorldPoints(Vector3Int cellPosition, int distance)
    {
        var result = new List<Vector3>();
        foreach (var cellPath in GetCellPaths(cellPosition, distance))
        {
            result.Add(cellPath.worldPosition);
        }
        return result;
    }

    private void AddIfWalkable(List<Vector3Int> list, Vector3Int cellPosition, int deltax, int deltay)
    {
        var cell = new Vector3Int(cellPosition.x + deltax, cellPosition.y + deltay, cellPosition.z);
        if (IsCellWalkable(cell))
            list.Add(cell);
    }
}