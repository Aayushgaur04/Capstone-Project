using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleUIManager : MonoBehaviour
{
    public static PuzzleUIManager Instance;

    public string puzzleID;

    [Header("Panels")]
    public GameObject instructionPanel;
    public Button nextButton;
    public GameObject puzzlePanel;
    public Button[] optionButtons;
    public GameObject resultPanel;
    public TextMeshProUGUI resultText;
    public Button okButton;

    [Header("Puzzle Data")]
    public int correctOptionIndex = 0;
    
    private Weapon weapon;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // cache your player control script (make sure this matches your actual class name)
        weapon = FindObjectOfType<Weapon>();

        instructionPanel.SetActive(false);
        puzzlePanel.SetActive(false);
        resultPanel.SetActive(false);

        nextButton.onClick.AddListener(OnNextClicked);
        okButton.onClick.AddListener(OnOkClicked);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            int idx = i;
            optionButtons[i].image.color = Color.white;
            optionButtons[i].onClick.AddListener(() => OnOptionSelected(idx));
        }
    }

    public void ShowInstruction()
    {
        instructionPanel.SetActive(true);
        Time.timeScale = 0f;

        // ** DISABLE player control while UI is up **
        if (weapon != null)
            weapon.enabled = false;
    }

    void OnNextClicked()
    {
        instructionPanel.SetActive(false);
        puzzlePanel.SetActive(true);
    }

    void OnOptionSelected(int idx)
    {
        if (idx == correctOptionIndex)
        {
            puzzlePanel.SetActive(false);
            resultText.text = "<color=yellow>Correct!</color>\nYou got collectible ×1";
            resultPanel.SetActive(true);
            GameManager.Instance.CompletePuzzle(puzzleID);

        }
        else
        {
            optionButtons[idx].image.color = Color.red;
            var ph = FindObjectOfType<Health>();
            if (ph != null) ph.TakeDamage(10);
        }
    }

    void OnOkClicked()
    {
        resultPanel.SetActive(false);

        if (weapon != null)
            weapon.enabled = true;

        Time.timeScale = 1f;
    }
}
