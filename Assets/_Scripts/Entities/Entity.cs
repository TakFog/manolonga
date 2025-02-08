using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Entity : MonoBehaviour
{
    public bool IsExecuting { get; protected set; }
    public abstract IEnumerator C_ExecuteChoice(Choice choice);
    public bool facingRight;
    
    [SerializeField] protected float MovementAnimationDuration;
    [SerializeField] protected Ease MovementAnimationEase;

    protected void FaceDirection(Vector3 destination)
    {
        if (destination.x > transform.position.x) transform.localScale = Rescale(transform.localScale, !facingRight);
        else if (destination.x < transform.position.x) transform.localScale = Rescale(transform.localScale, facingRight);
    }

    private Vector3 Rescale(Vector3 orig, bool negative)
    {
        if ((orig.x < 0) == negative) return orig;
        return new Vector3(-orig.x, orig.y, orig.z);
    }
}