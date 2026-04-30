using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    public Light2D renderLight;
    public float firstValue = 0f;      // Minimum intensity
    public float secondValue = 0.36f;  // Maximum intensity
    public float secondsBetweenFlickers = 1f; // Time between flickers

    void Start()
    {
        if (renderLight == null)
            renderLight = GetComponent<Light2D>();

        StartCoroutine(LightFlicker());
    }

    IEnumerator LightFlicker()
    {
        while (true)
        {
            renderLight.intensity = Random.Range(firstValue, secondValue);
            yield return new WaitForSeconds(secondsBetweenFlickers);
        }
    }
}