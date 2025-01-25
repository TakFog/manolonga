using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public bool IsExecuting { get; protected set; }
    public abstract IEnumerator C_ExecuteChoice(Choice choice);
}