using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Choice
{
    [FormerlySerializedAs("ActionType")] [FormerlySerializedAs("actionType")] public EntityActionType actionTypeType;
    [FormerlySerializedAs("endCell")] public Vector3Int EndCell;
    [FormerlySerializedAs("round")] public int Round;
}