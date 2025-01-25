using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Choice
{
    [FormerlySerializedAs("actionType")] public EntityAction ActionType;
    [FormerlySerializedAs("endCell")] public Vector3Int EndCell;
    [FormerlySerializedAs("round")] public int Round;
}