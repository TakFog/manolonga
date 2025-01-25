using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity : MonoBehaviour
{
    public bool IsExecuting { get; protected set; }
    public abstract IEnumerator C_ExecuteChoice(Choice choice);
    
    [SerializeField] protected float MovementAnimationDuration;
    [SerializeField] protected Ease MovementAnimationEase;
}