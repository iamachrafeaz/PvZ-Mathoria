using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// Manages the UI for test mini-games, including progress, feedback, and test-specific UI elements.
// Implements singleton pattern for global access.

public class TestManagerUI : MonoBehaviour
{
    public static TestManagerUI Instance;

    [SerializeField] GameObject Panel; // Main test panel
    [SerializeField] GameObject CanvasBackground; // Background for test UI

    [SerializeField] AudioSource CorrectAnswerSound; // Sound for correct answer
    [SerializeField] AudioSource WrongAnswerSound;   // Sound for wrong answer

    // Test-specific UI GameObjects
    public GameObject verticalOperationUI;
    public GameObject findCompositionUI;
    public GameObject wordProblemUI;

    // Progress bar UI elements
    [SerializeField] GameObject WonSuns;    // UI element showing suns won
    [SerializeField] GameObject ProgressBar; // Progress bar object

    float progressBarWidth; // Cached width of the progress bar
    public int operationCount = 0; // Number of operations/questions in the test

    // UI for checking answers
    public GameObject checkButton;
    public Sprite WrongIcon;
    public Sprite RightIcon;
    public GameObject CorrectionIcon;

    private IOperation currentOperation; // Current operation/question
    public GameObject currentTest;       // Current test UI object
    public ITestUI CurrentTestUI;        // Current test UI logic
    int AmoutOfSunsForEachCorrectAnswer = 50; 

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Add listener to check answer button
        checkButton.transform.GetComponent<Button>().onClick.AddListener(CheckAnswer);
    }

    void Update()
    {
        ToggleCanvas();
    }


    // Initializes the UI at the start of a test session.

    public void OnTestStart()
    {
        progressBarWidth = ProgressBar.transform.GetComponent<RectTransform>().rect.width;
        WonSuns.transform.GetComponent<TextMeshProUGUI>().text = "0";
        ProgressBar.transform.Find("Progress").GetComponent<RectTransform>().sizeDelta = new Vector2(
            0,
            ProgressBar.transform.GetComponent<RectTransform>().rect.height
        );
    }


    // Checks the user's answer, updates feedback and progress, and notifies the TestManager.

    void CheckAnswer()
    {
        checkButton.transform.GetComponent<Button>().interactable = false;

        (bool isCorrect, int PlayerAnswer) = currentTest.transform.GetComponent<ITestChecker>().CheckAnswer();

        if (isCorrect == true)
        {
            CorrectionIcon.GetComponent<Image>().sprite = RightIcon;
            CorrectAnswerSound.Play();
            UpdateProgressBar();
        }
        else
        {
            CorrectionIcon.GetComponent<Image>().sprite = WrongIcon;
            WrongAnswerSound.Play();
        }

        CorrectionIcon.SetActive(true);

        StartCoroutine(ExecuteAfterDelay(3f));

        TestManager.Instance.UserHasAnswered = true;
        TestManager.Instance.PlayerAnswer = (PlayerAnswer, isCorrect);
    }


    // Creates and displays the appropriate UI for the given test type and data.

    public void CreateTestUI(string TestType, IOperation data)
    {
        checkButton.transform.GetComponent<Button>().interactable = true;
        currentOperation = data;

        if (TestType.Equals("vertical_operations"))
        {
            currentTest = verticalOperationUI;
            CurrentTestUI = verticalOperationUI.transform.GetComponent<VerticalOperationUI>();
        }
        else if (TestType.Equals("find_compositions"))
        {
            currentTest = findCompositionUI;
            CurrentTestUI = findCompositionUI.transform.GetComponent<FindCompositionUI>();
        }
        else if (TestType.Equals("multi_step_problem"))
        {
            currentTest = wordProblemUI;
            CurrentTestUI = wordProblemUI.transform.GetComponent<WordProblemUI>();
        }

        CurrentTestUI.OperationToUI(data);
        TogglePanel(true);
    }


    // Waits for a delay, then cleans up the UI and marks the operation as answered.

    IEnumerator ExecuteAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        verticalOperationUI.transform.GetComponent<ITestChecker>().Clean();
        CorrectionIcon.SetActive(false);

        currentOperation.SetIsAnswered(true);
    }


    // Shows or hides the main test panel.

    public void TogglePanel(bool state)
    {
        Panel.SetActive(state);
        if (!state) operationCount = 0;
    }


    // Updates the progress bar and the suns won UI.

    void UpdateProgressBar()
    {
        RectTransform progressRect = ProgressBar.transform.Find("Progress").GetComponent<RectTransform>();

        float chunk = progressBarWidth / operationCount;

        progressRect.sizeDelta = new Vector2(progressRect.sizeDelta.x + chunk, progressRect.sizeDelta.y);

        WonSuns.transform.GetComponent<TextMeshProUGUI>().text = (int.Parse(WonSuns.transform.GetComponent<TextMeshProUGUI>().text) + AmoutOfSunsForEachCorrectAnswer).ToString();
    }


    // Returns the number of suns won so far.

    public int GetWonSuns()
    {
        return int.Parse(WonSuns.transform.GetComponent<TextMeshProUGUI>().text);
    }


    // Shows or hides the canvas background depending on the current scene.

    void ToggleCanvas()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            CanvasBackground.SetActive(true);
        }
        else
        {
            CanvasBackground.SetActive(false);
        }
    }
}
