using UnityEngine;
using System.Collections.Generic;

public class CoordManager : MonoBehaviour
{

    private static List<Vector2> wallCoordinates = new List<Vector2>();

    public static void AddWallCoordinate(Transform wallTransform)
    {
        if (wallTransform.CompareTag("Wall"))
        {
            Vector2 newCoordinate = new Vector2(wallTransform.position.x, wallTransform.position.z);
            wallCoordinates.Add(newCoordinate);
        }
    }

    public static List<Vector2> GetAllWallCoordinates()
    {
        return wallCoordinates;
    }

    public void PrintCoordsDebugLog()
    {
        Debug.Log("All Wall Coordinates (X, Z):");
        foreach (Vector2 coordinate in wallCoordinates)
        {
            Debug.Log("(" + coordinate.x + ", " + coordinate.y + ")");
        }
    }

    public void ClearAllCoordinates()
    {
        wallCoordinates.Clear();
        Debug.Log("Wall Coordinates Cleared.");
    }
}
