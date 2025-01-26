using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps; // Add this line

public class TeleportClickManager : MonoBehaviour
{
    public Camera camera;
    public Tilemap tilemap;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            
            if (hit.collider == null || !hit.collider.CompareTag("Teleport")) {
                return;
            }

            var screenToWorldPoint = camera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("screenToWorldPoint "+screenToWorldPoint);
            var cell = tilemap.WorldToCell(screenToWorldPoint);
            Debug.Log("cell "+cell);
            var position = TilemapManager.Instance.GetCellWorldCenterPosition(cell);
            Debug.Log("Teleport to "+position);
        }
    }
}
