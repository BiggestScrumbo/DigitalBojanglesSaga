using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool smoothMove = false;
    public float moveSpeed = 10f;
    public float moveRotateSpeed = 500f;

    Vector3 targetGridPos;
    Vector3 prevTargetGridPos;
    Vector3 targetRotation;

 
    private void Start()
    {
        targetGridPos = Vector3Int.RoundToInt(transform.position);

    }

    bool IsTeleporting = false;

    public void TeleportPlayer(Vector3 newPosition)
    {
        IsTeleporting = true;
        transform.position = newPosition;
    }

    private void FixedUpdate()
    {
        if (!IsTeleporting)
        {
            MovePlayer();
        }
        else
        {
            IsTeleporting = false;
            targetGridPos = Vector3Int.RoundToInt(transform.position);
        }
    }

    void MovePlayer()
    {

        List<Vector2> wallCoordinates = CoordManager.GetAllWallCoordinates();

        //check if neither the x nor z positions are in the wallCoordinates list
        if (!IsPositionInList(wallCoordinates, targetGridPos.x, targetGridPos.z))
        {
            prevTargetGridPos = targetGridPos;
            Vector3 targetPosition = targetGridPos;

            if (targetRotation.y > 270f && targetRotation.y < 361f)
                targetRotation.y = 0f;
            if (targetRotation.y < 0f)
                targetRotation.y = 270f;

            if (!smoothMove)
            {
                transform.position = targetPosition;
                transform.rotation = Quaternion.Euler(targetRotation);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * moveSpeed);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * moveRotateSpeed);
            }
        }
        else
        {
            targetGridPos = prevTargetGridPos;
        }
    }
    bool IsPositionInList(List<Vector2> coordinatesList, float x, float z)
    {
        foreach (Vector2 coordinate in coordinatesList)
        {
            if (Mathf.Approximately(coordinate.x, x) && Mathf.Approximately(coordinate.y, z))
            {
                return true; //coord in list
            }
        }
        return false; //coord not in list
    }


    public void RotateLeft() { if (AtRest) { targetRotation -= Vector3.up * 90f; SnapTargetGridPos(); } }
    public void RotateRight() { if (AtRest) { targetRotation += Vector3.up * 90f; SnapTargetGridPos(); } }
    public void MoveForward() { if (AtRest) { targetGridPos += transform.forward; SnapTargetGridPos(); } }
    public void MoveBackward() { if (AtRest) { targetGridPos -= transform.forward; SnapTargetGridPos(); } }
    public void MoveLeft() { if (AtRest) { targetGridPos -= transform.right; SnapTargetGridPos(); } }
    public void MoveRight() { if (AtRest) { targetGridPos += transform.right; SnapTargetGridPos(); } }

    void SnapTargetGridPos() { targetGridPos = new Vector3(Mathf.Round(targetGridPos.x), targetGridPos.y, Mathf.Round(targetGridPos.z)); }





    bool AtRest
    {
        get
        {
            if ((Vector3.Distance(transform.position, targetGridPos) < 0.05f) &&
                (Vector3.Distance(transform.eulerAngles, targetRotation) < 0.05f))
                return true;
            else
                return false;
                
        }
    }
}
