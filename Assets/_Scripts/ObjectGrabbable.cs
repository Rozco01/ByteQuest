using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour
{
    private Rigidbody objectRb;
    private Transform objectGrabPointTransform;
    
    private void Awake() {
        objectRb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform){
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRb.useGravity = false;
        objectRb.isKinematic = true;
    }

    public void Drop(){
        this.objectGrabPointTransform = null;
        objectRb.useGravity = true;
        objectRb.isKinematic = false;
    }

    private void FixedUpdate() {
        if(objectGrabPointTransform != null){
            float lerpSpeed = 15f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.fixedDeltaTime * lerpSpeed);
            objectRb.MovePosition(newPosition);
        }
    }
}
