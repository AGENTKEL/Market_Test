using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
[Header("Movement Settings")]
    public FixedJoystick joystick;
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;

    [Header("Camera Settings")]
    public Transform cameraTransform;
    public float sensitivity = 0.2f; // Sensitivity of swipe
    public float maxVerticalAngle = 80f; // Limits camera rotation
    private Vector2 lastTouchPosition;
    private bool isSwiping = false;
    private float verticalRotation = 0f;

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        HandleCameraRotation();
    }

    private void HandleMovement()
    {
        // Get joystick input
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        // Convert input into movement direction
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        moveDirection *= moveSpeed * Time.deltaTime;

        // Move using CharacterController
        characterController.Move(moveDirection);
    }

    private void HandleCameraRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detect if touch is on the right side of the screen
            if (touch.position.x > Screen.width / 2)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    lastTouchPosition = touch.position;
                    isSwiping = true;
                }
                else if (touch.phase == TouchPhase.Moved && isSwiping)
                {
                    Vector2 delta = touch.position - lastTouchPosition;
                    lastTouchPosition = touch.position;

                    // Rotate player horizontally
                    transform.Rotate(Vector3.up, delta.x * sensitivity);

                    // Rotate camera vertically
                    verticalRotation -= delta.y * sensitivity;
                    verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);
                    cameraTransform.localEulerAngles = new Vector3(verticalRotation, 0, 0);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isSwiping = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            other.GetComponent<Animator>().SetBool("Open", true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Door"))
        {
            other.GetComponent<Animator>().SetBool("Open", false);
        }
    }
}
