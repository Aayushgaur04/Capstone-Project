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
        if (player == null)
        {
            Debug.LogError("Player reference is missing!");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        Debug.Log("Current distance to player: " + distance);

        if (!gateOpened && distance < gateOpenDistance)
        {
            Debug.Log("Gate opening...");
            gateAnimator.SetBool("OpenGate", true);
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

        // Ensure we only get sprites from the player object (excluding gate)
        SpriteRenderer[] sprites = player.GetComponentsInChildren<SpriteRenderer>();
        Color[] originalColors = new Color[sprites.Length];

        // Store original colors
        for (int i = 0; i < sprites.Length; i++)
        {
            originalColors[i] = sprites[i].color;
        }

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, alpha);
            }

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure full transparency
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, 0f);
        }

        yield return new WaitForSeconds(0.5f);
        levelCompletePanel.SetActive(true);
    }


}
