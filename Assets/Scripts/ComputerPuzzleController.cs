using UnityEngine;

public class ComputerPuzzleController : MonoBehaviour
{
    [Header("References")]
    public Transform player;                 // Assign the player's transform.
    public GameObject interactPrompt;        // UI element that says "Press E to interact"
    public GameObject puzzleUIPanel;         // The puzzle UI panel (should be inactive by default)
    public GateController gateController;    // Reference to the gate controller

    private bool puzzleSolved = false;

    void Update()
    {
        if (!puzzleSolved)
        {
            // Check the distance between the player and this computer
            float distance = Vector3.Distance(player.position, transform.position);
            if (distance <= 1f)
            {
                // Show the interaction prompt
                interactPrompt.SetActive(true);

                // If player presses E, open the puzzle
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenPuzzle();
                }
            }
            else
            {
                interactPrompt.SetActive(false);
            }
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }

    void OpenPuzzle()
    {
        // Open the puzzle UI panel.
        puzzleUIPanel.SetActive(true);

        // (Optional) Freeze player movement if desired.
    }

    // This method is called by the puzzle UI manager when the correct answer is selected.
    public void PuzzleSolved()
    {
        puzzleSolved = true;
        puzzleUIPanel.SetActive(false);

        // Notify the gate to open.
        if (gateController != null)
        {
            gateController.OpenGate();
        }
    }
}
