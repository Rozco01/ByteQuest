using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject pointer;
    public GameObject pauseIMG;
    public GameObject botonPausa; // Agrega el botón del menú de pausa

    private void Start()
    {
        // Al inicio, ocultar el cursor del ratón
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        botonPausa.SetActive(false); // Deshabilita el botón al inicio
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Verifica la tecla "P" para pausar
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        pauseIMG.SetActive(true);
        pointer.SetActive(true);
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        isPaused = false;

        // Ocultar el cursor del ratón nuevamente
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Deshabilitar el botón del menú de pausa
        botonPausa.SetActive(false);
    }

    public void QuitToMenu(string sceneName)
    {
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
        SceneManager.LoadScene(sceneName);
    }

    void PauseGame()
    {
        pauseMenu.SetActive(true);
        pauseIMG.SetActive(false);
        pointer.SetActive(false);
        Time.timeScale = 0f; // Pausar el tiempo en el juego
        isPaused = true;

        // Mostrar el cursor del ratón
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Habilitar el botón del menú de pausa
        botonPausa.SetActive(true);
    }
}
