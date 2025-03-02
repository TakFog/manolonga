using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitManager : MyBehaviour
{
    public static ExitManager Instance { get; private set; }
    public int numberOfUsedExits;
    public List<Exit> ActiveExits = new List<Exit>(); 
    public List<Exit> ClosedExits = new List<Exit>();
    public List<Exit> OpenedExits => ActiveExits.Except(ClosedExits).ToList();
    
    private void Awake()
    {
        Instance = this;
        ActiveExits = new List<Exit>(FindObjectsByType<Exit>(FindObjectsSortMode.None))
            .OrderBy(e => e.transform.position.x).ThenBy(e => e.transform.position.y).ToList();
        //ShuffleList(ActiveExits);

        //for (int i = numberOfUsedExits; i < ActiveExits.Count; i++)
        //{
        //    ActiveExits[i].SetClosed();
        //    ClosedExits.Add(ActiveExits[i]);
        //}
    }

    protected override void OnEnableAfterStart()
    {
        CommunicationManager.Instance.OnOpenedExitsReceived += InitExits;
    }

    private void OnDisable()
    {
        CommunicationManager.Instance.OnOpenedExitsReceived -= InitExits;
    }

    private void InitExits(int[] opened)
    {
        for (int i = 0; i < ActiveExits.Count; i++)
        {
            if (!opened.Contains(i))
            {
                ClosedExits.Add(ActiveExits[i]);
                ActiveExits[i].SetClosed();
            }
        }
    }

    void ShuffleList(List<Exit> list)
    {
        for (int i = 0; i < list.Count - 1; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }

    public void CloseExit(Exit exit)
    {
        ClosedExits.Add(exit);
    } 

    public bool IsOpen(Exit exit)
    {
        return !ClosedExits.Contains(exit);
    }

    public bool AreAllClosed()
    {
        return ClosedExits.Count >= ActiveExits.Count;
    }

}
