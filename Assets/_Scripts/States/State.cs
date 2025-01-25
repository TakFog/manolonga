using UnityEngine;
public abstract class State
{
    public virtual void Enter()
    {
        Debug.Log("Enter " + GetType().Name);
    }
    public virtual void Update() { }
    public virtual void Exit()
    {
        Debug.Log("Exit " + GetType().Name);

    }
}