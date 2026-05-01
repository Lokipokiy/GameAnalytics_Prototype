using System.Collections;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static Unity.VisualScripting.Member;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    float moveInput;
    public Vector3 spawnPoint;

    //public TMP_Text scoreText;
    //public int score;

    //int gemValue = 5;

    public GemScript[] gemScript;
    public GoblinScript[] goblinScript;
    public SkeletonScript[] skeletonScript;
    public BatScript batScript;
    public DestroyInstruc destroyScript;

    public int health;
    public GameObject[] hearts;

    [SerializeField] GameObject bullet;
    [SerializeField] float bulletSpeed = 18.0f;
    bool fired = false;
    [SerializeField] Transform bulletspawnpoint;
    Vector3 mouseWP, rot;
    float rotZ;
    private Camera mainCam;
    [SerializeField] GameObject aim;

    //Audio
    //public AudioSource attack;
    //public AudioSource death;
    //public AudioSource collect;
    //public AudioSource hurt;
    //public AudioSource walk;
    //public AudioSource heal;

    BoxCollider2D Boxcollider;

    //Weapon Types
    bool melee = true;
    bool range = false;
    bool trap = false;

    bool facingRight = true;
    bool isCrouching = false;
    Rigidbody2D rb;

    Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int goblinLayer = LayerMask.GetMask("Goblin");
        spawnPoint = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Boxcollider = GetComponent<BoxCollider2D>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        health = 5;
    }

    // Update is called once per frame
    void Update()
    {
         rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocityY);

        MouseDetection();

        if (moveInput!=0)
         {
            anim.SetBool("isWalking", true);
         }
         else
         {
            anim.SetBool("isWalking", false);
         }
         

        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        if(Input.GetKey(KeyCode.Keypad1) && !melee)
        {
            melee = true;
        }
        else
        {
            melee = false;
        }

        if (Input.GetKey(KeyCode.Keypad2) && !range)
        {
            range = true;
        }
        else
        {
            range = false;
        }

        if (Input.GetKey(KeyCode.Keypad3) && !trap)
        {
            trap = true;
        }
        else
        {
            trap = false;
        }
        
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
           //.Play();
           if (isCrouching)
            {
                moveInput = 0f;
                return;
            }

            moveInput = ctx.ReadValue<Vector2>().x;
        }
        if (ctx.canceled)
        {
            moveInput = 0f;
            anim.SetBool("isWalking", false);
            //walk.Stop();
        }

    }

    public void idle(InputAction.CallbackContext ctx)
    {
        moveInput = 0f;
        anim.SetBool("isWalking", false);
    }

    public void Crouch(InputAction.CallbackContext ctx)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (ctx.performed)
        {
            isCrouching = true;
            anim.SetBool("Crouch", true );
            float x = Mathf.Sign(transform.localScale.x);
            collider.size = new Vector2(collider.size.x, collider.size.y / 2);
            moveInput = 0f;
            Debug.Log("Crouching");
        }
        else if (ctx.canceled)
        {
            isCrouching = false;
            collider.size = new Vector2(collider.size.x, collider.size.y * 2);
            anim.SetBool("Crouch", false);
            float x = Mathf.Sign(transform.localScale.x);
        }
    }


    public void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Health()
    {
        if (health > 0)
        {
            anim.SetTrigger("Hurt");
            //hurt.Play();
            health -= 1;
            if (health == 4)
            {
                hearts[4].SetActive(false);
            }
            if (health == 3)
            {
                hearts[3].SetActive(false);
            }
            if (health == 2)
            {
                hearts[2].SetActive(false);
            }
            if (health == 1)
            {
                hearts[1].SetActive(false);
            }
        }

        Debug.Log("Player Health: " + health.ToString());
        //hearts[health].SetActive(false);
        if (health <= 0)
        {
            StartCoroutine(DeathAnim());
        }
    }

    public void HealthRefill(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;

        Debug.Log("Attack");

        Vector2 direction = !facingRight ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + direction * 0.5f;

        Debug.DrawRay(origin, direction * 2f, Color.red, 0.5f);

        int skeletonLayer = LayerMask.GetMask("Skeleton");

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 2f, skeletonLayer);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);

            SkeletonScript skeleton = hit.collider.GetComponentInParent<SkeletonScript>();
            if (skeleton != null)
            {
                for (int i = 0; i < hearts.Length; i++)
                {
                    hearts[i].SetActive(true);
                }
                health = 5;
                anim.SetTrigger("Heal");
                //heal.Play();
                skeleton.Death();
            }
        }
     
    }

    public void Attack(InputAction.CallbackContext ctx)
    {
        if (melee == true)
        {
            if (!ctx.performed) return;
            moveInput = 0f;
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
            //attack.Play();
            Vector2 direction = !facingRight ? Vector2.left : Vector2.right;
            Vector2 origin = (Vector2)transform.position + direction * 0.5f;

            Debug.DrawRay(origin, direction * 2f, Color.red, 0.5f);

            int goblinLayer = LayerMask.GetMask("Goblin");

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, 3f, goblinLayer);

            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.name);

                GoblinScript goblin = hit.collider.GetComponentInParent<GoblinScript>();
                if (goblin != null)
                {
                    goblin.TakeDamage(50);

                }
                if (goblin.health <= 0)
                {
                    //score += 10;
                    //scoreText.text = "Score - " + score.ToString();
                }
            }
        }

        //if(range == true)
        //{
        //    if (!ctx.performed) return;
        //    moveInput = 0f;
        //    Debug.Log("Attack");
        //    anim.SetTrigger("Attack");

        //    if (!fired) StartCoroutine(FireProjectile());

        //    int goblinLayer = LayerMask.GetMask("Goblin");


        //    if (bullet.co)
        //    {
        //        Debug.Log("Hit: " + hit.collider.name);

        //        GoblinScript goblin = hit.collider.GetComponentInParent<GoblinScript>();
        //        if (goblin != null)
        //        {
        //            goblin.TakeDamage(50);

        //        }
        //        if (goblin.health <= 0)
        //        {
        //            //score += 10;
        //            //scoreText.text = "Score - " + score.ToString();
        //        }
        //    }
        //}


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RespawnZone"))
        {
            
            this.transform.position = spawnPoint;
            for (int i = 0; i < 6; i++)
            {
                gemScript[i].Reactivate();
                goblinScript[i].Reactivate();
                
            }
            for (int i = 0; i < 2; i++)
            {
                skeletonScript[i].Reactivate();
            }
            batScript.Reactivate();
            destroyScript.Destroy();
        }
        if (collision.gameObject.CompareTag("Gem"))
        {
            Debug.Log("Gem Collected");
            collision.gameObject.SetActive(false);
            //collect.Play();
            //score += gemValue;  
            //scoreText.text = "Score - " + score.ToString();
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Attack(new InputAction.CallbackContext());
        }
        if (collision.gameObject.CompareTag("Stalactite"))
        {
            Health();
            WaitForSeconds wait = new WaitForSeconds(0.5f);
        }
        if (collision.gameObject.CompareTag("Skeleton"))
        {
            HealthRefill(new InputAction.CallbackContext());
        }
    }

    
IEnumerator DeathAnim()
{
    //death.Play();
    anim.SetTrigger("Death");
    GetComponent<Collider>().enabled = false;
    yield return new WaitForSeconds(1.5f); // wait for animation
    SceneManager.LoadScene(4);
} 
private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (facingRight)
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 2F);
        }
        else
        {
            Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 2f);
        }
    }

    void MouseDetection()
    {
        mouseWP = mainCam.ScreenToWorldPoint(Input.mousePosition);
        rot = mouseWP - aim.transform.position;
        rotZ = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;
        aim.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    IEnumerator FireProjectile()
    {
        fired = true;
        var fb = Instantiate(bullet, bulletspawnpoint.position, aim.transform.rotation);
        fb.GetComponent<Rigidbody2D>().AddForce(rot * bulletSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(.15f);
        fired = false;
    }

}
