using UnityEngine;

public class AsteroidManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject asteroid;
    float minX = -14;
    float minY = -6;
    float maxX = 14;
    float maxY = 6;
    float Y;
    float X;
    int startingAsteroids = 12;
    public Transform spawnpoint;

    //Sprites
    //public Sprite[] asteroidsSprites;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnpoint = transform;
        while(startingAsteroids > 0)
        {
            X = Random.Range(minX, maxX); 
            Y = Random.Range(minY, maxY);
            //Debug.Log(Y);
            spawnpoint.transform.position = new Vector2(X, Y);
            var ia = Instantiate(asteroid, spawnpoint);
            ia.gameObject.transform.SetParent(null);
            startingAsteroids = startingAsteroids - 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void AsteriodSprite()
    //{
    //    int spriteCount = asteroidsSprites.Length;
    //    int choosenSprite = Random.Range(0, spriteCount);

    //    asteroid.GetComponent<SpriteRenderer>().sprite = asteroidsSprites[choosenSprite];
    //}
}
