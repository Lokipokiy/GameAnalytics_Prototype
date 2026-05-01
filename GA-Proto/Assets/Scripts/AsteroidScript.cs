using Unity.VisualScripting;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField]
    GameObject asteroid;

    public Player player;

    public GameObject scoreManager;
    ScoreScript scoreScript;
    //Sprites
    public Sprite[] asteroidsSprites;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AsteriodSprite();
        scoreManager = GameObject.FindWithTag("ScoreManager");
        scoreScript = scoreManager.GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(asteroid.gameObject.transform.position.y < -5)
        {
            asteroid.transform.position = new Vector2 (asteroid.transform.position.x, asteroid.transform.position.y + .1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            scoreScript.UpdateScore(5);
            Destroy(collision.gameObject);
            Destroy(asteroid);
        }
    }
    public void AsteriodSprite()
    {
        int spriteCount = asteroidsSprites.Length;
        int choosenSprite = Random.Range(0, spriteCount);
        asteroid.GetComponent<SpriteRenderer>().sprite = asteroidsSprites[choosenSprite];
    }
}
