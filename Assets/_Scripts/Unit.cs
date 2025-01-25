using UnityEngine;using UnityEngine.EventSystems;

public class Unit : MonoBehaviour
{
    
}

public abstract class Command
{
    public abstract void Do();
    public abstract void Undo();
}
