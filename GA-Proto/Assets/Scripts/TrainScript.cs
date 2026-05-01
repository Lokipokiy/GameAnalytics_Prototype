using System.Collections;
using UnityEngine;

public class TrainScript : MonoBehaviour
{
    [SerializeField]
    GameObject trainCar;
    GameObject[] trainCars;
    [SerializeField]
    float difficultyModifier = 1.0f;
    [SerializeField]
    float size = 5;
    [SerializeField]
    float spawnSize = 5;
    [SerializeField]
    public float remaining = 5;
    bool isSpawning = false;
    public bool isEndLevel = false;

    public AudioClip levelComplete;
    public AudioSource audioSource;

    Transform spawnpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnpoint = transform;
        size = size * difficultyModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnSize >= 1 && !isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnPart());
            spawnSize = spawnSize - 1;
        }

        if(remaining <= 0 && !isSpawning)
        {
            isEndLevel = true;
            Debug.Log("level ended.");
        }

        trainCars = GameObject.FindGameObjectsWithTag("TrainCars");
        remaining = trainCars.Length;

        if(isEndLevel)
        {
            audioSource.PlayOneShot(levelComplete);
            isEndLevel = false;
            difficultyModifier = difficultyModifier + 0.1f;
            size = (size * difficultyModifier);
            spawnSize = size;
            //Debug.Log(size);    
        }

    }
    IEnumerator SpawnPart()
    {
        yield return new WaitForSeconds(.15f);
        var tc = Instantiate(trainCar, spawnpoint.position, spawnpoint.rotation);
        isSpawning = false;
    }
}
