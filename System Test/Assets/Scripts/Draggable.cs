using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool isDragging;
    public bool isOverlapping;
    private Vector3 posBeforeDragging;
    private Rigidbody rb;
    
    void Start()
    {
        isDragging = false;
        isOverlapping = false;
        posBeforeDragging = transform.position;
        rb = GetComponent<Rigidbody>();
    }
    
    
    void Update()
    {
        if (isDragging)
        {
            rb.isKinematic = false;
            Plane plane = new Plane(transform.up, transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out float hitDist))
            {
                Vector3 targetPoint = ray.GetPoint(hitDist);
                transform.position = targetPoint;
            }
        }
    }

    private void OnMouseDown()
    {
        if (EditManager.isEditing)
        {
            isDragging = true;
        }
    }

    private void OnMouseUp()
    {
        if (isOverlapping)
        {
            transform.position = posBeforeDragging;
        }
        else
        {
            posBeforeDragging = transform.position;
        }
        
        isDragging = false;
        rb.isKinematic = true;
    }

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Module"))
        {
            isOverlapping = true;
        }
        else
        {
            isOverlapping = false;
        }
    }
}
