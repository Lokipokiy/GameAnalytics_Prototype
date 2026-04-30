using UnityEngine;
using System.Collections;

public class Stalactite : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 startPos;
    Animator anim;
    public AudioSource falling;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
       
        startPos = transform.position;
        rb.gravityScale = 0;
    }

    public void StartFall()
    {
        StartCoroutine(Fall());
    }

    IEnumerator Fall()
    {
        anim.SetTrigger("Wiggle");
        falling.Play();
        yield return new WaitForSeconds(2f);
        rb.gravityScale = 3;
        yield return new WaitForSeconds(2f);

        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.zero;
        transform.position = startPos;
    }
}
