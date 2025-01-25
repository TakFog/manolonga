using System;
using UnityEngine;

public class EntitySetup : MonoBehaviour
{
    Child child;
    Monster monster;

    private void Start()
    {
        child = Globals.Child;
        monster = Globals.Monster;
        
        bool enableChild = Globals.PlayerType == PlayerType.Child;
        bool enableMonster = Globals.PlayerType == PlayerType.Monster;
        
        child.GetComponentInChildren<AudioEntity>().enabled = enableChild;
        monster.GetComponentInChildren<AudioEntity>().enabled = enableMonster;

        child.GetComponentInChildren<Camera>().gameObject.SetActive(enableChild);
        monster.GetComponentInChildren<Camera>().gameObject.SetActive(enableMonster);
        
        
    }
}
