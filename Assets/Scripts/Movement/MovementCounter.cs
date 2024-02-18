using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCounter : MonoBehaviour
{
    private int movementCount = 0;
    private int globalMovementCount = 0;

    public void IncrementMovementCount()
    {
        movementCount++;
        globalMovementCount++;
        //Debug.Log("Movement Count: " + movementCount);
        //add that other thing here
    }

    public int GetMovementCount()
    {
        return movementCount;
    }

    public void ResetMovementCount()
    {
        movementCount = 0;
    }

    public int GetGlobalMovementCount()
    {
        return globalMovementCount;
    }
}
