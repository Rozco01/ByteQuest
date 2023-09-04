using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] Transform holdArea;
    [SerializeField] private GameObject heldObj;
    [SerializeField] private Rigidbody heldObjRb;
    [SerializeField] private LayerMask pickupLayerMask;

    [Space]         
    
    [Header(" Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
        if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, pickupRange, pickupLayerMask))
            {
                PickupObject(hit.transform.gameObject);
            }
        }
        else
        {
            Drop();
        }
       }
       if (heldObj != null)
       {
           MoveObject();
            // Agregar rotación cuando se presionan las teclas E y Q
            float rotationSpeed = 200.0f; // Ajusta la velocidad de rotación según tus necesidades
            if (Input.GetKey(KeyCode.E))
            {
                RotateObject(Vector3.up, rotationSpeed);
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                RotateObject(Vector3.up, -rotationSpeed);
            }
       }

       
    }

    void PickupObject(GameObject pickObj)
    {
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.velocity = Vector3.zero;
            heldObjRb.drag = 20;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRb.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void Drop()
    {
        heldObjRb.useGravity = true;
        heldObjRb.drag = 1;
        heldObjRb.constraints = RigidbodyConstraints.None;

        heldObjRb.transform.parent = null;
        heldObj = null;
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position).normalized;
            heldObjRb.AddForce(moveDirection * pickupForce);
        }
    }

    void RotateObject(Vector3 axis, float speed)
    {
        // Rotar el objeto en el eje especificado a la velocidad dada
        heldObj.transform.Rotate(axis, speed * Time.deltaTime);
    }
}