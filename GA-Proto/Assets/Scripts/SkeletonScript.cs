using System.Collections;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void Death()
    {
        StartCoroutine(DeathCon());
    }

    public void Reactivate()
    {
        Debug.Log("Skeleton Respawns!");
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(10f, 50), transform.position.y, transform.position.z);
    }

    IEnumerator DeathCon()
    {
        Debug.Log("Skeleton Dies!");
        anim.SetTrigger("Collect");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
