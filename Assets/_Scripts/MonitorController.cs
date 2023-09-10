using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public bool triggerText = true;
    private bool isInsideTrigger = false;
    public GameObject monitorDialogue;

    void Start()
    {
        monitorDialogue.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = true;
            Debug.Log("Entraste en el trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
            monitorDialogue.SetActive(false);
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Presionaste la tecla R dentro del trigger.");
            if (triggerText)
            {
                // MonitorController no necesita una función ShowDialogPanel, simplemente activa el panel directamente.
                monitorDialogue.SetActive(true);
                triggerText = false;
            }
        }
    }
}
