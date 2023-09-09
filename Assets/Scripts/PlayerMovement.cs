using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float maxVelocity;

    [SerializeField] private float rotationSpeed;

    private Camera mainCamera;
    private Rigidbody rb;

    private Vector3 movementDirection;
    
    void Start()
    {
        mainCamera = Camera.main;
        rb= GetComponent<Rigidbody>();

    }

    void Update()
    {
        ProcessInput();
        HandleRotation();     
    }

    private void HandleRotation()
    {
        if (rb.velocity == Vector3.zero)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(rb.velocity, Vector3.back);
        transform.rotation =  Quaternion.Lerp(
            transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        ClampPosition();
        ProcessPhysics();       
    }

    private void ProcessPhysics()
    {
        if (movementDirection == Vector3.zero) { return; }

        rb.AddForce(movementDirection * force, ForceMode.Force);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    private void ProcessInput()
    {
        if (Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);

            movementDirection = worldPosition - transform.position;
            movementDirection.z = 0;
            //magnitude doesnt matter only direction
            movementDirection.Normalize();
            Debug.Log("direction changed");
        }
        else 
        {
            movementDirection = Vector3.zero;
        }

    }

    void ClampPosition()
    {
        Vector3 newPosition = transform.position;
        Vector3 viewPortPosition = mainCamera.WorldToViewportPoint(transform.position);

        if(viewPortPosition.x > 1) 
        {
            newPosition.x = -newPosition.x + 0.1f;
        }
        else if(viewPortPosition.x < 0)
        {
            newPosition.x = -newPosition.x - 0.1f;
        }
        if (viewPortPosition.y > 1)
        {
            newPosition.y = -newPosition.y + 0.1f;
        }
        else if (viewPortPosition.y < 0)
        {
            newPosition.y = -newPosition.y - 0.1f;
        }

        transform.position = newPosition;
                     
    }
}
