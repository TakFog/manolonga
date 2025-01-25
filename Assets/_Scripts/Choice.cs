using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Choice
{
    [FormerlySerializedAs("actionTypeType")] [FormerlySerializedAs("ActionType")] public EntityActionType actionType;
    [FormerlySerializedAs("PositionPath")] [FormerlySerializedAs("CellPath")] [FormerlySerializedAs("endCell")] public List<Vector3> PositionsPath;
    [FormerlySerializedAs("round")] public int Round;
}