using UnityEngine;

public class GemScript : MonoBehaviour
{
    public int gemValue = 5;
    public int score;

    public void Reactivate()
    {
        gameObject.SetActive(true);
        transform.position = new Vector3(Random.Range(10, 50), transform.position.y, transform.position.z);
    }
}
