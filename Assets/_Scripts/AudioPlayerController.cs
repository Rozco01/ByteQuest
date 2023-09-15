using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepSound;
    public float stepInterval = 0.2f; // Intervalo entre los sonidos de pasos (ajusta este valor según tus necesidades).
    public CharacterController characterController; // Referencia al Character Controller.

    private float stepTimer = 0f;
    private bool isGrounded = false; // Variable para rastrear si el personaje está en el suelo.

    private void Start()
    {

    }

    private void Update()
    {
        // Verifica si se presiona alguna tecla de movimiento horizontal o vertical.
        bool moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Si se está moviendo (presionando alguna tecla), controla la reproducción de sonidos de pasos.
        if (moving)
        {


                // Si ha pasado el intervalo de tiempo, reproduce un sonido de paso.
                if (stepTimer >= stepInterval)
                {
                    PlayStepSound();
                    stepTimer = 0f; // Reinicia el temporizador.
                }
            
        }
        else
        {
            // Detén el sonido de pasos si no se está moviendo.
            audioSource.Stop();
        }
    }

    void PlayStepSound()
    {
        if (stepSound != null)
        {
            audioSource.clip = stepSound;
            audioSource.Play();
        }
    }
}
