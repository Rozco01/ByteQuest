using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip stepSound;
    public AudioClip jumpSound; // Nuevo sonido para el salto.
    public float stepInterval = 0.2f; // Intervalo entre los sonidos de pasos (ajusta este valor según tus necesidades).
    public CharacterController characterController; // Referencia al Character Controller.

    private float stepTimer = 0f;
    private bool isGrounded = false; // Variable para rastrear si el personaje está en el suelo.

    private void Start()
    {
        // Asegúrate de asignar el componente AudioSource, los AudioClips y el Character Controller en el Inspector.
        if (audioSource == null)
        {
            Debug.LogError("El componente AudioSource no está asignado.");
        }

        if (stepSound == null)
        {
            Debug.LogError("El AudioClip de pasos no está asignado.");
        }

        if (jumpSound == null)
        {
            Debug.LogError("El AudioClip de salto no está asignado.");
        }

        if (characterController == null)
        {
            Debug.LogError("El Character Controller no está asignado.");
        }
    }

    private void Update()
    {
        // Verifica si el Character Controller está en contacto con el suelo.
        isGrounded = characterController.isGrounded;

        // Verifica si se presiona la tecla de salto (Space) y el personaje está en el suelo.
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            // Reproduce el sonido de salto.
            PlayJumpSound();
        }

        // Verifica si se presiona alguna tecla de movimiento horizontal o vertical.
        bool moving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        // Si se está moviendo (presionando alguna tecla), controla la reproducción de sonidos de pasos.
        if (moving)
        {
            // Verifica si el personaje está en el suelo o si está subiendo (para evitar que se solapen los sonidos).
            if (isGrounded || characterController.velocity.y > 0)
            {
                // Actualiza el temporizador.
                stepTimer += Time.deltaTime;

                // Si ha pasado el intervalo de tiempo, reproduce un sonido de paso.
                if (stepTimer >= stepInterval)
                {
                    PlayStepSound();
                    stepTimer = 0f; // Reinicia el temporizador.
                }
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

    void PlayJumpSound()
    {
        if (jumpSound != null)
        {
            audioSource.clip = jumpSound;
            audioSource.Play();
        }
    }
}
