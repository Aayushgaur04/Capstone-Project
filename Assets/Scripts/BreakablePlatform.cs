using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public float breakDelay = 1.5f;
    public float fadeDuration = 0.5f;
    public float respawnDelay = 5f;

    private SpriteRenderer sr;
    private Collider2D col;
    private bool isBreaking = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreaking) return;

        if (collision.collider.CompareTag("Player") && collision.relativeVelocity.y <= 0)
        {
            isBreaking = true;
            Invoke(nameof(FadeOutAndDisable), breakDelay);
        }
    }

    void FadeOutAndDisable()
    {
        StartCoroutine(FadePlatform(1f, 0f, fadeDuration, () =>
        {
            col.enabled = false;
            Invoke(nameof(Respawn), respawnDelay);
        }));
    }

    void Respawn()
    {
        col.enabled = true;
        StartCoroutine(FadePlatform(0f, 1f, fadeDuration, () =>
        {
            isBreaking = false;
        }));
    }

    System.Collections.IEnumerator FadePlatform(float startAlpha, float endAlpha, float duration, System.Action onComplete)
    {
        float elapsed = 0f;
        Color color = sr.color;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            sr.color = color;
            elapsed += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        sr.color = color;
        onComplete?.Invoke();
    }
}
