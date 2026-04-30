using System.Collections;
using UnityEngine;

public class GoblinScript : MonoBehaviour
{
    public int health = 100;

    public PlayerScript playerScript;
    public Vector2 startPos;
    public Vector2 leftPos;
    public Vector2 rightPos;

    public float moveDistance = 3;

    public float attackRange = .5f;

    public float detectionRange = 5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 1.5f;

    private float nextAttackTime;
    private Coroutine patrolRoutine;

    Animator anim;

    //public AudioSource death;
    //public AudioSource attack;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = new Vector2(transform.position.x, transform.position.y);
        leftPos = new Vector2(startPos.x + moveDistance, startPos.y);
        rightPos = new Vector2(startPos.x - moveDistance, startPos.y);


        // = GetComponent<Animator>();
        patrolRoutine = StartCoroutine(MoveEnemy(leftPos));
    }

    // Update is called once per frame

    void Update()
    {
        float distance = Vector2.Distance(transform.position, playerScript.transform.position);

        if (distance <= detectionRange)
        {
            // Stop patrolling
            if (patrolRoutine != null)
            {
                StopCoroutine(patrolRoutine);
                patrolRoutine = null;
            }

            // Face player
            if ((playerScript.transform.position.x > transform.position.x && transform.localScale.x < 0) ||
                (playerScript.transform.position.x < transform.position.x && transform.localScale.x > 0))
            {
                FlipEnemy();
            }

            // Move toward player if not in attack range
            if (distance > attackRange)
            {
                Vector3 targetPosition = new Vector3(
                    playerScript.transform.position.x,  
                    transform.position.y,               
                    transform.position.z
                );

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    moveSpeed * Time.deltaTime
                );
            }
            else
            {
                // Attack with cooldown
                if (Time.time >= nextAttackTime)
                {
                    Attack();
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
        else
        {
            // Resume patrol if player leaves range
            if (patrolRoutine == null)
            {
                patrolRoutine = StartCoroutine(MoveEnemy(leftPos));
            }
        }
    }


    public void Attack()
    {

        if (Vector2.Distance(transform.position, playerScript.transform.position) <= attackRange)
        {
            anim.SetTrigger("Attack");
           // attack.Play();
            playerScript.Health();
            WaitForSeconds wait = new WaitForSeconds(0.5f);
        }
    }



    public void Death()
    {
        Debug.Log("Goblin Dies!");
        //death.Play();
        anim.SetTrigger("Death");
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"Goblin takes {damage} damage!");
        if (health > 0)
        {
            health -= damage;
            if (health <= 0)
            {
                Debug.Log("Goblin health is zero or below.");
                Death();
            }
        }
    }

    IEnumerator MoveEnemy(Vector2 target)
    {
        anim.SetBool("isWalking", true);

        while (Mathf.Abs(target.x - transform.position.x) > 0.1f)
        {
            float direction = Mathf.Sign(target.x - transform.position.x);

            transform.position += new Vector3(
                direction * moveSpeed * Time.deltaTime,
                0f,
                0f
            );

            yield return null;
        }

        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(1f);

        FlipEnemy();

        Vector2 nextTarget = target == leftPos ? rightPos : leftPos;
        patrolRoutine = StartCoroutine(MoveEnemy(nextTarget));
    }

    private void FlipEnemy()
    {
        Vector3 eScale = transform.localScale;
        eScale.x *= -1;
        transform.localScale = eScale;
    }

    public void Reactivate()
    {
        Debug.Log("Goblin Respawns!");
        FlipEnemy();
        gameObject.SetActive(true);
        health = 100;
        transform.position = new Vector3(Random.Range(10f, 50), transform.position.y, transform.position.z);
        patrolRoutine = StartCoroutine(MoveEnemy(leftPos));
    }

}
