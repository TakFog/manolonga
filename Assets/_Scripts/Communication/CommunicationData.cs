
[System.Serializable]
public class CommunicationData
{
    public Choice Monster;
    public Choice Child;
    public bool hasMonster;
    public bool hasChild;
}

[System.Serializable]
public class CreateGameData
{
    public string gameid;
}

[System.Serializable]
public class InitInputData
{
    public int numExits;
    public int numOpenExits;
    public int numMonsterSpawns;
    public int numChildSpawns;
}


[System.Serializable]
public class InitOutputData
{
    public int[] openExits;
    public int monsterSpawn;
    public int childSpawn;
}

