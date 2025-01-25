using System;
using System.Collections.Generic;
using UnityEngine;

public class Child : Entity
{
    [Range(1, 5)] public int WalkDistance;
    [Range(1, 5)] public int RunDistance;
}