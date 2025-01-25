using System;
using System.Collections.Generic;
using UnityEngine;

public class HighlightManager : MonoBehaviour
{
    public static HighlightManager Instance;
    
    [SerializeField] GameObject highlightObjectPrefab;
    Dictionary<Vector3Int, GameObject> highlightObjects = new Dictionary<Vector3Int, GameObject>();
    List<Vector3Int> highlightedCells = new List<Vector3Int>();

    private void Awake()
    {
        Instance = this;
    }

    public void HighlightCell(Vector3Int cellPosition)
    {
        if(highlightedCells.Contains(cellPosition))
            return;
        var highlightedObject = Instantiate(highlightObjectPrefab, TilemapManager.Instance.GetCellWorldCenterPosition(cellPosition), Quaternion.identity);
        highlightObjects.Add(cellPosition, highlightedObject);
        highlightedCells.Add(cellPosition);
    }
    public void UnhighlightCell(Vector3Int cellPosition)
    {
        if(!highlightedCells.Contains(cellPosition))
            return;
        Destroy(highlightObjects[cellPosition]);
        highlightObjects.Remove(cellPosition);
        highlightedCells.Remove(cellPosition);
    }
    
    public void HighlightCells(List<Vector3Int> cells)
    {
        foreach (var cell in cells)
        {
            HighlightCell(cell);
        }
    }
    public void UnhighlightCells(List<Vector3Int> cells)
    {
        foreach (var cell in cells)
        {
            UnhighlightCell(cell);
        }
    }
}
