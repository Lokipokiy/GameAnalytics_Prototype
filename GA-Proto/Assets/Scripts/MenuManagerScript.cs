using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManagerScript : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene("MenuPickPlayer");
    }
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        Application.Quit();
    }

}
