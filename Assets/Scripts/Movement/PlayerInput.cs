using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode turnLeft = KeyCode.Q;
    public KeyCode turnRight = KeyCode.E;

    PlayerMovement controller;
    bool canMove = true;
    float delay = 0.2f; //adjust this value for the delay between inputs

    private void Awake()
    {
        controller = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (controller.MovementEnabled == true)
        {
            HandleMovementInput();
            HandleRotationInput();
        }
    }

    void HandleMovementInput()
    {
        if (canMove)
        {
            if (Input.GetKey(forward)) StartCoroutine(MoveWithDelay(controller.MoveForward));
            if (Input.GetKey(backward)) StartCoroutine(MoveWithDelay(controller.MoveBackward));
            if (Input.GetKey(left)) StartCoroutine(MoveWithDelay(controller.MoveLeft));
            if (Input.GetKey(right)) StartCoroutine(MoveWithDelay(controller.MoveRight));
        }
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(turnLeft)) controller.RotateLeft();
        if (Input.GetKey(turnRight)) controller.RotateRight();
    }

    IEnumerator MoveWithDelay(System.Action action)
    {
        canMove = false;
        action.Invoke();
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
