
using System.Collections.Generic;
using UnityEngine;

public class CellPath
{
    public Vector3Int cell;
    public Vector3 worldPosition;
    public List<Vector3Int> path;
    public List<Vector3> worldPath;

    public CellPath(Vector3Int cell, Vector3 worldPosition)
    {
        this.cell = cell;
        this.worldPosition = worldPosition;
    }
}
