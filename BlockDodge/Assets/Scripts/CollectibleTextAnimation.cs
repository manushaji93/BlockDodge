using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleTextAnimation : MonoBehaviour {

    float fadeTime, moveDist;
    Vector3 finalPos;

	// Use this for initialization
	void Start () {

        fadeTime = 1f;

        moveDist = 3f;

        finalPos = new Vector3(transform.position.x, transform.position.y + moveDist, transform.position.z);

        StartCoroutine(FadeOut());

	}

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, finalPos, Time.deltaTime / fadeTime);
    }

    // Update is called once per frame
    IEnumerator FadeOut () {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Cache the current color of the material, and its initial opacity.
        Color color = spriteRenderer.color;
        float startOpacity = color.a;
        float targetOpacity = 0f;

        // Track how many seconds we've been fading.
        float t = 0;

        while (t < fadeTime)
        {
            // Step the fade forward one frame.
            t += Time.deltaTime;
            // Turn the time into an interpolation factor between 0 and 1.
            float blend = Mathf.Clamp01(t / fadeTime);

            // Blend to the corresponding opacity between start & target.
            color.a = Mathf.Lerp(startOpacity, targetOpacity, blend);

            // Apply the resulting color to the material.
            spriteRenderer.color = color;

            // Wait one frame, and repeat.
            yield return null;
        }

        Destroy(gameObject);

    }
}
