using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
                UIManager.Instance.ShowExitDirection();
                break;
        }
        IsExecuting = false;
    }
    
    public IEnumerator C_Walk(List<Vector3> positionsPath)
    {
        for (int i = 0; i < positionsPath.Count; i++)
        {
            yield return transform.DOMove(positionsPath[i], MovementAnimationDuration/positionsPath.Count).SetEase(MovementAnimationEase).WaitForCompletion();
        }
    }
    public IEnumerator C_Run(List<Vector3> positionsPath)
    {
        for (int i = 0; i < positionsPath.Count; i++)
        {
            yield return transform.DOMove(positionsPath[i], MovementAnimationDuration/positionsPath.Count).SetEase(MovementAnimationEase).WaitForCompletion();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            GameOverManager.Instance.MonsterWins();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            GameOverManager.Instance.ChildWins();
        }
    }
}