using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPicker : MonoBehaviour
{
    public void V1()
    {
        SceneManager.LoadScene("GameV1");
    }
    public void V2()
    {
        SceneManager.LoadScene("GameV2");
    }
    public void V3()
    {
        SceneManager.LoadScene("GameV3");
    }
}
