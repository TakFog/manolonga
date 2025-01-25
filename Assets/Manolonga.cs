using UnityEngine;
using System.Collections.Generic;

public class HexPlayerMovement : MonoBehaviour
{
    // Hex grid settings
    public float hexWidth = 1.732f; // Horizontal distance between hex centers
    public float hexHeight = 1.5f; // Vertical distance between hex centers

    // Player's current position in axial coordinates (q, r)
    public Vector2Int currentHexPosition = new Vector2Int(0, 0);

    // List to store the movement path
    private List<Vector2Int> movementPath = new List<Vector2Int>();

    void Update()
    {
        HandleClick();
    }

    void HandleClick()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button clicked
        {
            // Get mouse position in world coordinates
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0; // Ensure z is 0 for 2D

            // Convert world position to hex coordinates
            Vector2Int clickedHex = WorldToHex(mouseWorldPos);

            // Check if the clicked hex is adjacent to the last step
            if (IsAdjacent(clickedHex))
            {
                // Add the clicked hex to the movement path
                movementPath.Add(clickedHex);
                Debug.Log($"Step {movementPath.Count}: {clickedHex}");
            }
            else
            {
                Debug.Log("Invalid step: Not adjacent to the last step.");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Press Space to confirm and move
        {
            if (movementPath.Count > 0)
            {
                StartCoroutine(MoveAlongPath());
            }
            else
            {
                Debug.Log("No steps recorded.");
            }
        }
    }

  bool IsAdjacent(Vector2Int hex)
{
    if (movementPath.Count == 0)
    {
        // First step must be adjacent to the player's current position
        return GetNeighbors(currentHexPosition).Contains(hex);
    }
    else
    {
        // Subsequent steps must be adjacent to the last step in the path
        Vector2Int lastStep = movementPath[movementPath.Count - 1];
        return GetNeighbors(lastStep).Contains(hex);
    }
}

    List<Vector2Int> GetNeighbors(Vector2Int hex)
    {
        // Neighbors in axial coordinates
        return new List<Vector2Int>
        {
            hex + new Vector2Int(1, 0),   // Right
            hex + new Vector2Int(1, -1),  // Up-Right
            hex + new Vector2Int(0, -1),  // Up-Left
            hex + new Vector2Int(-1, 0),  // Left
            hex + new Vector2Int(-1, 1),  // Down-Left
            hex + new Vector2Int(0, 1)    // Down-Right
        };
    }

    System.Collections.IEnumerator MoveAlongPath()
    {
        foreach (var step in movementPath)
        {
            // Move the player to the step
            Vector3 worldPosition = HexToWorld(step);
            transform.position = worldPosition;

            // Update the player's current hex position
            currentHexPosition = step;

            // Wait for a short time before moving to the next step
            yield return new WaitForSeconds(0.5f);
        }

        // Clear the movement path after moving
        movementPath.Clear();
    }

    Vector3 HexToWorld(Vector2Int hexPosition)
    {
        float x = hexWidth * (hexPosition.x + hexPosition.y * 0.5f);
        float y = hexHeight * hexPosition.y * 0.58f;
        return new Vector3(x, y, 0);
    }

   Vector2Int WorldToHex(Vector3 worldPosition)
{
    float q = (worldPosition.x / hexWidth) - (worldPosition.y / (hexHeight * 2f));
    float r = worldPosition.y / (hexHeight * 0.75f);

    // Use a more robust rounding method
    int qRounded = Mathf.RoundToInt(q);
    int rRounded = Mathf.RoundToInt(r);

    // Validate the rounded coordinates
    Vector2Int roundedHex = new Vector2Int(qRounded, rRounded);
    Vector3 roundedWorldPos = HexToWorld(roundedHex);

    // Check if the rounded world position is close to the original world position
    if (Vector3.Distance(worldPosition, roundedWorldPos) > 0.1f) // Adjust threshold as needed
    {
        // If not, find the closest hexagon
        Vector2Int closestHex = roundedHex;
        float closestDistance = Vector3.Distance(worldPosition, roundedWorldPos);

        foreach (var neighbor in GetNeighbors(roundedHex))
        {
            Vector3 neighborWorldPos = HexToWorld(neighbor);
            float distance = Vector3.Distance(worldPosition, neighborWorldPos);
            if (distance < closestDistance)
            {
                closestHex = neighbor;
                closestDistance = distance;
            }
        }

        return closestHex;
    }

    return roundedHex;
}




    void OnGUI()
{
    int gridRadius = 5; // Adjust as needed

    for (int q = -gridRadius; q <= gridRadius; q++)
    {
        for (int r = -gridRadius; r <= gridRadius; r++)
        {
            if (Mathf.Abs(q + r) <= gridRadius) // Ensure we're within the hex grid bounds
            {
                Vector2Int hex = new Vector2Int(q, r);
                Vector3 worldPos = HexToWorld(hex);

                // Convert world position to screen position
                Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

                // Draw the hex coordinates on the screen
                string coordinateText = $"({hex.x}, {hex.y})";
                GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 20), coordinateText);
            }
        }
    }
}


}