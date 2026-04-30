using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player; // Reference to the player's transform
    public Vector3 offset; // Offset between camera and player

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       offset = transform.position - player.position; // Calculate initial offset
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset; // Update camera position to follow the player
    }
}
