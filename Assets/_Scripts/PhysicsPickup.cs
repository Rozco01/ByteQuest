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
    private CodeBlock codeBlock;
    [SerializeField] private Rigidbody heldObjRb;
    [SerializeField] private LayerMask pickupLayerMask;

    [Space]

    [Header(" Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    public bool IsholdingObj = false;

    [Header("Canvas Pointer")]
    public Image pointerImg;

    private void Start()
    {
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
            codeBlock = pickObj.GetComponent<CodeBlock>();
            pointerImg.enabled = true;
            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.velocity = Vector3.zero;
            heldObjRb.drag = 20;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            codeBlock.isHolding = true;
            heldObjRb.transform.parent = holdArea;
            heldObj = pickObj;
            Debug.Log("pick" + codeBlock.isHolding);
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
        codeBlock.isHolding = false;
        Debug.Log("Dropped" + codeBlock.isHolding);
    }

    void MoveObject()
    {
        if (Vector3.Distance(holdArea.position, heldObj.transform.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position).normalized;
            heldObjRb.AddForce(moveDirection * pickupForce);

            // Alinea la rotación del objeto con la del holdArea
            Quaternion targetRotation = Quaternion.LookRotation(holdArea.forward, Vector3.up);
            heldObjRb.MoveRotation(targetRotation);
        }
    }

    void RotateObject(Vector3 axis, float speed)
    {
        // Rotar el objeto en el eje especificado a la velocidad dada
        heldObj.transform.Rotate(axis, speed * Time.deltaTime);
    }
}
