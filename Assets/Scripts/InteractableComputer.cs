using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableComputer : MonoBehaviour
{
    public GameObject promptPrefab;   // “Press E to interact”
    private GameObject promptInstance;
    private bool playerInRange = false;

    void Awake()
    {
        // ensure trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // spawn the prompt just above the computer
            promptInstance = Instantiate(
                promptPrefab,
                transform.position,
                Quaternion.identity
            );
            // keep it in world space, or parent to the computer if you want it to move with it
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptInstance) Destroy(promptInstance);
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (promptInstance) Destroy(promptInstance);
            PuzzleUIManager.Instance.ShowInstruction();
        }
    }
}
