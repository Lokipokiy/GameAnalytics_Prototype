using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public void PauseFunc(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isPaused = !isPaused;
            if (isPaused)
                PauseMenu();
            else
                Resume();
        }
    }
    void PauseMenu()
    {
      //  Cursor.visible = true;
      //  Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Pauses all audio
    }
    public void Resume()
    {
     //   Cursor.visible = false;
      //  Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Resumes all audio
    }
}


