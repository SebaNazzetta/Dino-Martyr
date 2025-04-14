using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : MonoBehaviour
{
    public float delayBeforeFade = 0f;       // Tiempo antes de comenzar a desvanecerse
    public float fadeDuration = 0.5f;            // Tiempo que tarda en desvanecerse

    private SpriteRenderer spriteRenderer;
    private bool isFading = false;
    private float fadeTimer = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isFading && collision.gameObject.CompareTag("Player"))
        {
            Invoke(nameof(StartFading), delayBeforeFade);
            isFading = true;
        }
    }

    void StartFading()
    {
        fadeTimer = fadeDuration;
    }

    void Update()
    {
        if (fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);
            Color color = spriteRenderer.color;
            color.a = alpha;
            spriteRenderer.color = color;

            if (fadeTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
