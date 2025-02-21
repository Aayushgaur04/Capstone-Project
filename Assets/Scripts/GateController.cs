using UnityEngine;
using System.Collections;

public class GateController : MonoBehaviour
{
    [Header("Gate Settings")]
    public GameObject gateObject;          // The visual/physical gate
    public Collider2D blockingCollider;    // The collider that blocks the player
    public GameObject levelCompletePanel;  // Level Complete UI panel
    public Animator gateAnimator;          // Gate animation controller
    public Transform player;               // Player transform
    public float gateOpenDistance = 3f;    // Distance to trigger the gate animation
    public float fadeDuration = 1.5f;      // Time taken to fade out player

    private bool isOpen = false;
    private bool gateOpened = false;
    private SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!gateOpened && Vector2.Distance(transform.position, player.position) < gateOpenDistance)
        {
            gateAnimator.SetBool("OpenGate",true); // This triggers the animation only when player is close
            gateOpened = true;
        }
    }

    public void OpenGate()
    {
        isOpen = true;
        if (blockingCollider != null)
        {
            blockingCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isOpen && collision.CompareTag("Player"))
        {
            StartCoroutine(FadeOutPlayer());
        }
    }

    IEnumerator FadeOutPlayer()
    {
        float timer = 0f;
        Color originalColor = playerSprite.color;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            timer += Time.deltaTime;
            yield return null;
        }

        playerSprite.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        yield return new WaitForSeconds(0.5f);
        levelCompletePanel.SetActive(true);
    }
}
