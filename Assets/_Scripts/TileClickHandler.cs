using UnityEngine;
using System.Collections.Generic; // Add this line

public class TileClickHandler : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera
    public TilemapManager tilemapManager; // Reference to the TilemapManager

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
          
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; 

            Vector3Int cellPosition = tilemapManager.GetCellAtWorldPosition(mouseWorldPos);

            Vector3 cellWorldCenterPos = tilemapManager.GetCellWorldCenterPosition(cellPosition);

            bool isWalkable = tilemapManager.IsCellWalkable(cellPosition);

            Debug.Log($"Clicked on cell: {cellPosition}");
            Debug.Log($"World center position: {cellWorldCenterPos}");
            Debug.Log($"Is walkable: {isWalkable}");

            int distance = 2;
            List<Vector3Int> nearbyCells = tilemapManager.GetNearCells(cellPosition, distance);
            Debug.Log($"Nearby cells within distance {distance}:");
           
        }
    }
}