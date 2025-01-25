
using System.Collections.Generic;
using UnityEngine;

public class CellPath
{
    public Vector3Int cell;
    public Vector3 worldPosition;
    public List<Vector3Int> path;
    public List<Vector3> worldPath;
    
    public int Size {get {return path.Count;}}

    public CellPath(Vector3Int cell, Vector3 worldPosition)
    {
        this.cell = cell;
        this.worldPosition = worldPosition;
        path = new List<Vector3Int>();
        path.Add(cell);
        worldPath = new List<Vector3>();
        worldPath.Add(worldPosition);
    }

    public CellPath(Vector3Int cell, List<Vector3Int> path)
    {
        this.cell = cell;
        worldPosition = Vector3.zero;
        this.path = path;
        worldPath = null;
    }

    public CellPath(Vector3Int cell, Vector3 worldPosition, List<Vector3Int> path, List<Vector3> worldPath)
    {
        this.cell = cell;
        this.worldPosition = worldPosition;
        this.path = path;
        this.worldPath = worldPath;
    }
}
