using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Credits()
    {
        SceneManager.LoadScene(1);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    public void Instructions()
    {
        SceneManager.LoadScene(3);
    }
    public void Death()
    {
        SceneManager.LoadScene(4);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
