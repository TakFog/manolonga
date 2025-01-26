using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance { get; private set; }
    [SerializeField] int numberOfUsedExits;
    public List<Exit> ActiveExits = new List<Exit>(); 
    public List<Exit> ClosedExits = new List<Exit>();
    public List<Exit> OpenedExits => ActiveExits.Intersect(ClosedExits).ToList();
    
    private void Awake()
    {
        Instance = this;
        ActiveExits = new List<Exit>(FindObjectsOfType<Exit>());
        int numberOfUnusedExits = ActiveExits.Count - numberOfUsedExits;
        ShuffleList(ActiveExits);
        
        for(int i = 0; i < numberOfUnusedExits; i++)
        {
            ActiveExits[i].gameObject.SetActive(false);
            ActiveExits.RemoveAt(i);
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

}
