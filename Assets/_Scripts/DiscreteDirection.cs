using UnityEngine;

public class DiscreteDirection
{
    public static readonly float SQRT_3_2 = Mathf.Sqrt(3) / 2;
    
    // Define the 6 directions corresponding to the sides of a hexagon
    private static readonly Vector2[] hexagonDirections = new Vector2[]
    {
        new Vector2(1, 0),                // Right
        new Vector2(-1, 0),               // Left
        new Vector2(0, 1),                          // Up
        new Vector2(0, -1),                         // Down
        new Vector2(0.5f, SQRT_3_2),   // Top-right
        new Vector2(-0.5f, SQRT_3_2),  // Top-left
        new Vector2(0.5f, -SQRT_3_2),  // Bottom-right
        new Vector2(-0.5f, -SQRT_3_2)  // Bottom-left
    };
    
    // Define the 8 directions corresponding to the sides of an octagon
    private static readonly Vector2[] octagonDirections = new Vector2[]
    {
        new Vector2(1, 0),                          // Right
        new Vector2(1 / Mathf.Sqrt(2), 1 / Mathf.Sqrt(2)),   // Top-right
        new Vector2(0, 1),                          // Up
        new Vector2(-1 / Mathf.Sqrt(2), 1 / Mathf.Sqrt(2)),  // Top-left
        new Vector2(-1, 0),                         // Left
        new Vector2(-1 / Mathf.Sqrt(2), -1 / Mathf.Sqrt(2)),  // Bottom-left
        new Vector2(0, -1),                         // Down
        new Vector2(1 / Mathf.Sqrt(2), -1 / Mathf.Sqrt(2))    // Bottom-right
    };

    public static Cardinal ToCardinal(Vector2 dir)
    {
        if (IsTop(dir))
        {
            if (IsLeft(dir)) return Cardinal.NorthWest;
            if (IsRight(dir)) return Cardinal.NorthEast;
            return Cardinal.North;
        } 
        if (IsBottom(dir))
        {
            if (IsLeft(dir)) return Cardinal.SouthWest;
            if (IsRight(dir)) return Cardinal.SouthEast;
            return Cardinal.South;
        }
        if (IsLeft(dir)) return Cardinal.West;
        return Cardinal.East;
    }

    public static bool IsTop(Vector2 direction)
    {
        return direction.y > 0;
    }

    public static bool IsBottom(Vector2 direction)
    {
        return direction.y < 0;
    }

    public static bool IsVCenter(Vector2 direction)
    {
        return direction.y == 0;
    }

    public static bool IsHCenter(Vector2 direction)
    {
        return direction.x == 0;
    }

    public static bool IsLeft(Vector2 direction)
    {
        return direction.x < 0;
    }

    public static bool IsRight(Vector2 direction)
    {
        return direction.x > 0;
    }

    // Given source and destination, returns the direction on the unit hexagon
    public static Vector2 GetOctagonDirection(Vector2 source, Vector2 destination)
    {
        // Calculate the direction from source to destination
        Vector2 direction = (destination - source).normalized;

        // Variable to hold the closest direction index
        int closestIndex = 0;
        float closestDot = float.MinValue;

        // Iterate over all 6 hexagon directions and find the closest
        for (int i = 0; i < octagonDirections.Length; i++)
        {
            // Dot product to find the closest direction
            float dotProduct = Vector2.Dot(direction, octagonDirections[i]);

            if (dotProduct > closestDot)
            {
                closestDot = dotProduct;
                closestIndex = i;
            }
        }

        // Return the closest direction
        return octagonDirections[closestIndex];
    }
    
    
    // Given source and destination, returns the direction on the unit hexagon
    public static Vector2 GetHexagonDirection(Vector2 source, Vector2 destination, Vector2 scale)
    {
        // Calculate the direction from source to destination
        Vector2 direction = (destination - source).normalized;

        // Variable to hold the closest direction index
        int closestIndex = 0;
        float closestDot = float.MinValue;

        // Iterate over all 6 hexagon directions and find the closest
        for (int i = 0; i < hexagonDirections.Length; i++)
        {
            // Dot product to find the closest direction
            float dotProduct = Vector2.Dot(direction, hexagonDirections[i] * scale);

            if (dotProduct > closestDot)
            {
                closestDot = dotProduct;
                closestIndex = i;
            }
        }

        // Return the closest direction
        return hexagonDirections[closestIndex] * scale;
    }
}

