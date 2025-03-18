using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private float pickupRange = 3f;
    [SerializeField] private Transform handPosition;
    [SerializeField] private GameObject throwButton;

    private PickableObject currentObject = null;

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    PickableObject pickable = hit.collider.GetComponent<PickableObject>();

                    if (pickable != null && currentObject == null)
                    {
                        float distance = Vector3.Distance(transform.position, pickable.transform.position);
                        if (distance <= pickupRange)
                        {
                            PickUpObject(pickable);
                        }
                    }
                }
            }
        }
    }

    private void PickUpObject(PickableObject obj)
    {
        currentObject = obj;
        obj.PickUp(handPosition);
        throwButton.SetActive(true);
    }

    public void Throw()
    {
        if (currentObject != null)
        {
            currentObject.Throw();
            currentObject = null;
            throwButton.SetActive(false);
        }
    }
}
