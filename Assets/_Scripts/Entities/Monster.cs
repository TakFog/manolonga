using UnityEngine;

public class Monster : Entity
{
    [Range(1, 5)] public int MoveDistance;
    [Range(1, 5)] public int AttackDistance;
}