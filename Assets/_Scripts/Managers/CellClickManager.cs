using System;
using UnityEngine;
using System.Collections.Generic; // Add this line

public class CellClickManager : MonoBehaviour
{
    public static CellClickManager Instance;
    public event Action<Vector3Int> OnCellClicked;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
          
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; 

            Vector3Int cellPosition = TilemapManager.Instance.GetCellAtWorldPosition(mouseWorldPos);

            Vector3 cellWorldCenterPos = TilemapManager.Instance.GetCellWorldCenterPosition(cellPosition);

            bool isWalkable = TilemapManager.Instance.IsCellWalkable(cellPosition);

            Debug.Log($"Clicked on cell: {cellPosition}");
            Debug.Log($"World center position: {cellWorldCenterPos}");
           
            OnCellClicked?.Invoke(cellPosition);
        }
    }
}
