using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Monster : Entity
{
    [Range(1, 5)] public int MoveDistance;
    [Range(1, 5)] public int AttackDistance;

    [SerializeField] private GameObject hand;
    private List<GameObject> instantiatedHands;

    [Header("Animations")] private Animator animator;
    [Range(0,3)] public float AttackAnimationDuration;
    [Range(0,3)] public float HandAnimationDuration;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        Globals.Monster = this;
    }

    public IEnumerator C_ExecutePriorityChoice(Choice choice)
    {
        if (choice.actionType == EntityActionType.Attack)
        {
            yield return StartCoroutine(C_Attack(choice.PositionsPath));
        }
    }
    
    public override IEnumerator C_ExecuteChoice(Choice choice)
    {
        IsExecuting = true;
        switch (choice.actionType)
        {
            case EntityActionType.Move:
                yield return StartCoroutine(C_Move(choice.PositionsPath));
                break;
        }
        IsExecuting = false;
    }

    private IEnumerator C_Move(List<Vector3> positionsPath)
    {
        animator.SetBool("Walk", true);
        for (int i = 0; i < positionsPath.Count; i++)
        {
            FaceDirection(positionsPath[i]);
            yield return transform.DOMove(positionsPath[i], MovementAnimationDuration/positionsPath.Count).SetEase(MovementAnimationEase).WaitForCompletion();
        }
        animator.SetBool("Walk", false);
    }
    private IEnumerator C_Attack(List<Vector3> positionsPath)
    {
        animator.SetBool("Grab", true);
        instantiatedHands = new List<GameObject>();
        foreach (var position in positionsPath)
        {
            instantiatedHands.Add(Instantiate(hand, position, Quaternion.identity, transform));
            yield return new WaitForSeconds(HandAnimationDuration);
        }
    }
    
    public IEnumerator C_EndPriorityChoice(Choice choice)
    {
        if (choice.actionType == EntityActionType.Attack)
        {
            instantiatedHands.Reverse();
            foreach (var iHand in instantiatedHands)
            {
                iHand.GetComponentInChildren<Animator>().SetFloat("SpeedSign", -1);
                Destroy(iHand);
                yield return new WaitForSeconds(HandAnimationDuration);
            }
            instantiatedHands.Clear();
            animator.SetBool("Grab", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            other.GetComponent<Exit>().CloseExit();
            if (ExitManager.Instance.AreAllClosed())
                GameOverManager.Instance.MonsterWins();
        }
    }
}