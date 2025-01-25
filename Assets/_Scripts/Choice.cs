using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Choice
{
    public EntityActionType actionType;
    public Vector3[] positionsPath;
    public List<Vector3> PositionsPath
    {
        get => positionsPath?.ToList();
        set => positionsPath = value?.ToArray();
    }
}