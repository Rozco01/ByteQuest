using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Canvas Pointer")]
    public Image pointerImg;

    private void Start() {
        pointerImg.enabled = false;
    }
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
        }


    }

    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            pointerImg.enabled = true;
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
        pointerImg.enabled = false;
    }

    void MoveObject()
    {
        if (Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
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