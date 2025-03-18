using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    private Rigidbody rb;
    private Transform target; // Target position (Player's hand)
    private bool isBeingHeld = false;
    private bool isPassedTruck = false;

    [Header("Pickup Settings")]
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isBeingHeld && target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * moveSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime * rotationSpeed);
        }
    }

    public void PickUp(Transform hand)
    {
        target = hand;
        isBeingHeld = true;
        rb.isKinematic = true;
    }

    public void Throw()
    {
        isBeingHeld = false;
        target = null;
        rb.isKinematic = false;
        rb.AddForce(Camera.main.transform.forward * 5f, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPassedTruck)
            return;
        if (other.CompareTag("Truck"))
        {
            other.GetComponent<Truck>().IncrementCount();
            isPassedTruck = true;
        }
    }
}
