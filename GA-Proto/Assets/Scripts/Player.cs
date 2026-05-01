using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    [SerializeField]
    GameObject ship;
    float moveInputX;
    float moveInputY;
    float moveSpeed = 14.0f;
    Rigidbody2D rb2d;

    [SerializeField]
    GameObject bullet;

    public Transform spawnPoint;
    bool fired = false;

    [SerializeField]
    float bulletSpeed = 18.0f;

    int lives = 3;
    public GameObject[] livesIMG;
    bool canTakeDamage = true;
    public int armor = 2;

    public AudioSource source;
    public AudioClip damage;
    public AudioClip bulletFired;

    //Animation Controls
    Animator anim;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawnPoint = transform;
    }
    private void Update()
    {
        if (lives == 3) { }
        else if (lives == 2)
        {
            livesIMG[2].SetActive(false);
        }
        else if (lives == 1)
        {
            livesIMG[1].SetActive(false);
        }
        else if (lives == 0)
        {
            livesIMG[0].SetActive(false);
            SceneManager.LoadScene("GameOver");
        }

        }
    public void Move(InputAction.CallbackContext ctx)
    {
        moveInputX = ctx.ReadValue<Vector2>().x;
        moveInputY = ctx.ReadValue<Vector2>().y;
    }
    private void FixedUpdate()
    {
        //Tilting right
        if(Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Right", true);
        }
        else
        {
            anim.SetBool("Right", false);
        }

        //Tilting left
        if(Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Left", true);
        }
        else
        {
            anim.SetBool("Left", false);
        }

        //Change sprite moving up
        if(Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Foward", true);
        }
        else
        {
            anim.SetBool("Foward", false);
        }

        if (ship.transform.position.y < 0 || moveInputY < 0) rb2d.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);
        else rb2d.linearVelocity = new Vector2(moveInputX * moveSpeed, 0 * moveSpeed);
    }
    public void Shoot(InputAction.CallbackContext ctx)
    {
        if (!fired) StartCoroutine(FireProjectile());
    }
    IEnumerator FireProjectile()
    {
        fired = true;
        var fb = Instantiate(bullet, spawnPoint.position, spawnPoint.rotation);
        fb.GetComponent<Rigidbody2D>().AddForce(spawnPoint.up * bulletSpeed, ForceMode2D.Impulse);
        source.PlayOneShot(bulletFired);
        if(bullet.transform.position.y > ship.transform.position.y - 6)
        {
            Destroy(fb);
        }

        yield return new WaitForSeconds(.3f);
        fired = false;
    }
    IEnumerator TakeDamage()
    {
        source.PlayOneShot(damage);
        if (canTakeDamage)
        {
            if(armor > 0)
            {
                armor -= 1;
            }
            else
            {
                canTakeDamage = false;
                lives = lives - 1;
            }
        }
        yield return new WaitForSeconds(2);
        canTakeDamage = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TrainCars")
        {
            StartCoroutine(TakeDamage());
        }
    }

}
