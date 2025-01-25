using UnityEngine.Serialization;

[System.Serializable]
public class CommunicationData
{
    [FormerlySerializedAs("Monster")] public Choice MonsterChoice;
    [FormerlySerializedAs("Child")] public Choice ChildChoice;
    public bool hasMonster;
    public bool hasChild;
}