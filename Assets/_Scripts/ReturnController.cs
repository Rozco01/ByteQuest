using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnController : MonoBehaviour
{
    public Vector3 returnPositionPlayer, returnPositionCodeBlock;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = returnPositionPlayer;
        }else if (other.gameObject.tag == "CodeBlock") {
            other.gameObject.transform.position = returnPositionCodeBlock;
        }    
    }
}
