using UnityEngine;

public class PuzzleUIManager : MonoBehaviour
{
    [Header("Puzzle Settings")]
    [Tooltip("Index of the correct answer (0, 1, 2, or 3)")]
    public int correctAnswerIndex = 2; // Example: third option is correct

    [Header("References")]
    public ComputerPuzzleController computerPuzzleController; // Reference to notify when puzzle is solved

    // This method is called by the answer buttons.
    // For each button, set the OnClick() event to call SubmitAnswer with the appropriate index.
    public void SubmitAnswer(int answerIndex)
    {
        if (answerIndex == correctAnswerIndex)
        {
            Debug.Log("Correct Answer!");
            computerPuzzleController.PuzzleSolved();
        }
        else
        {
            Debug.Log("Wrong Answer! Try again.");
        }
    }

}
