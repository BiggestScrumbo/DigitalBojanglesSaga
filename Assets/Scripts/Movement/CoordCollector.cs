using UnityEngine;

public class CoordCollector : MonoBehaviour
{
    void Start()
    {
        CoordManager.AddWallCoordinate(transform);
    }
}
