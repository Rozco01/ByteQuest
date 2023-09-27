using UnityEngine;
using TMPro;

public class MonitorController : MonoBehaviour
{
    private bool isInsideTrigger = false;
    private bool hasTriggered = false;
    public GameObject monitorDialogue;
    public string[] linesMonitor;
    public DialogueScript dialogueScript;
    public TextMeshProUGUI TextMonitor;

    void Start()
    {
        monitorDialogue.SetActive(false);
        TextMonitor.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = true;
            Debug.Log("Entraste en el trigger.");
            TextMonitor.text = "Presiona R para interactuar.";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInsideTrigger = false;
            monitorDialogue.SetActive(false);
            hasTriggered = false;
            TextMonitor.text = "";
        }
    }

    private void Update()
    {
        if (isInsideTrigger && Input.GetKeyDown(KeyCode.R) && !hasTriggered)
        {
            Debug.Log("Presionaste la tecla R dentro del trigger.");
            hasTriggered = true;
            monitorDialogue.SetActive(true);
            dialogueScript.writeArray(linesMonitor);
            TextMonitor.text = "";
        }
    }
}
