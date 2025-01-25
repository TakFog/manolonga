using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class Child : Entity
{
    [Range(1, 5)] public int WalkDistance;
    [Range(1, 5)] public int RunDistance;

    private void Awake()
    {
        Globals.Child = this;
    }

    public override IEnumerator C_ExecuteChoice(Choice choice)
    {
        IsExecuting = true;
        switch (choice.actionType)
        {
            case EntityActionType.Walk:
                yield return StartCoroutine(C_Walk(choice.PositionsPath));
                break;
            case EntityActionType.Run:
                yield return StartCoroutine(C_Run(choice.PositionsPath));
                break;
            case EntityActionType.CheckWind:
                break;
        }
        IsExecuting = false;
    }
    
    public IEnumerator C_Walk(List<Vector3> positionsPath)
    {
        throw new NotImplementedException();
        foreach (var cell in positionsPath)
        {
            
        }
    }
    public IEnumerator C_Run(List<Vector3> positionsPath)
    {
        throw new NotImplementedException();
    }
    
}