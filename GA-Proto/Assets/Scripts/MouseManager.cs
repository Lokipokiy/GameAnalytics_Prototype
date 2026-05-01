using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MouseManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (SceneManager.GetSceneByName("Game") == SceneManager.GetActiveScene()) Cursor.visible = false;
        else { Cursor.visible = true; }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
