using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueScript : MonoBehaviour
{
    public MonitorController monitorController; // Agrega una referencia al panel de diálogo.
    public TextMeshProUGUI dialogueText;
    public string[] lines;
    public float textSpeed = 1f;
    int index;
    private bool isWriting = false; // Variable para rastrear si se está escribiendo el texto.

    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        isWriting = false;
        index = 0;

        StartDialogue(); // Asegúrate de llamar a StartDialogue para iniciar la escritura.
    }

    // Update is called once per frame
    void Update()
    {
        if (isWriting)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                // Detener la escritura si se presiona R.
                StopAllCoroutines();
                dialogueText.text = lines[index];
                isWriting = false;
            }
        }
        else
        {
            if (index == 0)
            {
                NextLine();
            }
            else if (index < lines.Length)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    // Iniciar la escritura de la siguiente línea.
                    NextLine();
                }
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        isWriting = true;
        StartCoroutine(WriteLine());
    }

    IEnumerator WriteLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isWriting = false;
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            isWriting = true;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
