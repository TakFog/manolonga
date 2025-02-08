using UnityEngine.Serialization;

[System.Serializable]
public class CommunicationData
{
    public Choice Monster;
    public Choice Child;
    public bool hasMonster;
    public bool hasChild;
}

[System.Serializable]
public class InitData
{
    public string gameid;
}