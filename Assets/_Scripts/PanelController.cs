using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{
    public List<GameObject> objectsInPanel = new List<GameObject>();
    public int numberOfObjectsInPanel;
    public bool isCorrect = false;

    // Variables para el material correcto y el material incorrecto
    public Material correctMaterial;
    public Material incorrectMaterial;

    // Referencia al Animator del botón
    public Animator buttonAnimator;
    // Referencia al Animator de la puerta
    public Animator doorAnimator;

    private bool isRaycastHitting = false;

    void Update()
    {
        // Comprueba el número de objetos en el panel
        CheckNumberOfObjects();
        // Comprueba si el jugador hizo clic derecho mientras el rayo está colisionando con el objeto
        if (isRaycastHitting && Input.GetMouseButtonDown(0))
        {
            // Activa la animación de presionar el botón
            if (buttonAnimator != null)
            {
                buttonAnimator.SetBool("IsPress", true);
            }

            if (isCorrect)
            {
                // Si es correcto, cambia el material y activa la animación de la puerta
                ChangeMaterial(correctMaterial);
                OpenDoor();
            }
            else
            {
                // Si no es correcto, cambia el material a rojo
                ChangeMaterial(incorrectMaterial);
            }
        }
        else
        {
            // Reinicia la animación de presionar el botón
            if (buttonAnimator != null)
            {
                buttonAnimator.SetBool("IsPress", false);
            }
        }
    }

    // Método para cambiar el material del objeto
    void ChangeMaterial(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = newMaterial;
        }
    }

    // Método para activar la animación de la puerta
    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("isOpen");
        }
    }

    // Cuando un rayo entra en colisión con el objeto
    private void OnMouseEnter()
    {
        isRaycastHitting = true;
    }

    // Cuando un rayo sale de colisión con el objeto
    private void OnMouseExit()
    {
        isRaycastHitting = false;
    }

    void CheckNumberOfObjects()
    {
        // Comprueba si el número de objetos en el panel es igual al número de objetos requeridos
        if (numberOfObjectsInPanel == objectsInPanel.Count)
        {
            // Si es así, establece el interruptor en true
            isCorrect = true;
        }
        else
        {
            // Si no, establece el interruptor en false
            isCorrect = false;
        }
    }

}
