using UnityEngine;
using System.Collections;

public class StalactiteFalling : MonoBehaviour
{
    public GameObject[] stalactites;
    public float minDelay = 2f;
    public float maxDelay = 4f;

    void Start()
    {
      
        StartCoroutine(RandomFall());
    }

    IEnumerator RandomFall()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

            int index = Random.Range(0, stalactites.Length);
            Stalactite single = stalactites[index].GetComponent<Stalactite>();


            if (single != null)
            {
                single.StartFall();

            }

        }
    }
}
