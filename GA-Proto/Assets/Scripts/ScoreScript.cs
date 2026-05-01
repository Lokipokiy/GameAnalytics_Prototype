using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;
    public int score;
    public TMP_Text scoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        score = 0;
    }
    private void Update()
    {
        if (SceneManager.GetSceneByBuildIndex(0) == SceneManager.GetActiveScene() && score != 0) score = 0;

        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TMP_Text>();
        //score = score + 1;
        scoreText.SetText("Score: " + score.ToString());
    }
    // Update is called once per frame
    public void UpdateScore(int value)
    {
        score = score + value;
    }
}
