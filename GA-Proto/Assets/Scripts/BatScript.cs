using System.Collections;
using UnityEngine;

public class BatScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 5f;
    public GameObject player;
    public float[] offset;


    public AudioSource bat;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = new float[waypoints.Length];

        for (int i = 0; i < waypoints.Length; i++)
        {
            offset[i] = waypoints[i].position.x - player.transform.position.x;
        }
        StartCoroutine(Attack());
    }

   // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].position = new Vector3(
                player.transform.position.x + offset[i],
                waypoints[i].position.y,
                waypoints[i].position.z
            );
        }
    }


    IEnumerator Attack()
    {
        while (true)
        {
            bat.Play();
            yield return new WaitForSeconds(4f);
            bat.Play();
           
            while (Vector2.Distance(transform.position, waypoints[1].position) >= 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[1].position, speed * Time.deltaTime);
                yield return null;
            }

            //yield return new WaitForSeconds(.5f);
            bat.Play();
            while (Vector2.Distance(transform.position, waypoints[2].position) >= 0.001f)
            {
                transform.position = Vector2.MoveTowards(transform.position, waypoints[2].position, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = waypoints[0].position;
        }
    }

    public void Reactivate()
    {
        this.transform.position = waypoints[0].position;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Bat hit Player");
            PlayerScript player = collision.GetComponent<PlayerScript>();
            player.Health();
            WaitForSeconds wait = new WaitForSeconds(1.5f);
        }
    }
}
