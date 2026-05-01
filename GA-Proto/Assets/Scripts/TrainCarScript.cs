using System.Collections;
using UnityEngine;

public class TrainCarScript : MonoBehaviour
{
    [SerializeField]
    GameObject trainCar;
    [SerializeField]
    float speed = 8;

    int dirX = -1;
    int dirY = -1;
    Rigidbody2D rb2d;

    public GameObject asteroid;
    Transform spawnPoint;

    public Player player;

    bool isFacingLeft = false;
    public bool exploded = false;

    public GameObject scoreManager;
    ScoreScript scoreScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreCollision(trainCar.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        spawnPoint = transform;
        scoreManager = GameObject.FindWithTag("ScoreManager");
        scoreScript = scoreManager.GetComponent<ScoreScript>();
    }

    // Update is called once per frame
    void Update()
    {
        trainCar.GetComponent<SpriteRenderer>().flipX = isFacingLeft;

    }
    private void FixedUpdate()
    {
        rb2d.linearVelocity = new Vector2(dirX * speed, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            dirX = -dirX;
            isFacingLeft = !isFacingLeft;
            if (trainCar.transform.position.y >= 6 || trainCar.transform.position.y <= -6)
            {
                dirY = -dirY;
                trainCar.transform.position = new Vector2(trainCar.transform.position.x,
                                               trainCar.transform.position.y + dirY);
            }
            else trainCar.transform.position = new Vector2(trainCar.transform.position.x,
                                               trainCar.transform.position.y + dirY);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            exploded = true;
            var ia = Instantiate(asteroid, spawnPoint.gameObject.transform);
            ia.gameObject.transform.SetParent(null);
            scoreScript.UpdateScore(20);
            Destroy(collision.gameObject);
            Destroy(trainCar);
        }
    }

}
