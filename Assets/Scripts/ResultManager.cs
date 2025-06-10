using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    // Stores test results and the order of mini-games
    public List<TestResult> testResults;
    public List<object> miniGamesOrder;

    // Prefabs for displaying individual result lines and the parent container
    [SerializeField] GameObject ResultLinePrefab;
    [SerializeField] GameObject ResultComponent;

    // Called when the script instance is being loaded
    void Awake()
    {
        // Retrieve test results and game order from TestManager
        testResults = TestManager.Instance.GetTestResult();
        miniGamesOrder = TestManager.Instance.Tests.miniGamesOrder;
    }

    // Initializes the result screen with each mini-game's score
    void Start()
    {
        foreach (string miniGame in miniGamesOrder)
        {
            int count = 0; // Total questions for this mini-game
            int NumberOfCorrectAnswers = 0; // Correct answers for this mini-game

            foreach (var result in testResults)
            {
                if (result.testName == miniGame.ToString())
                {
                    count++;
                    if (result.isCorrect)
                    {
                        NumberOfCorrectAnswers++;
                    }
                }
            }

            // Create a result line UI element and fill in the name and score
            var e = Instantiate(ResultLinePrefab, ResultComponent.transform);
            e.transform.Find("TestName").GetComponent<TextMeshProUGUI>().text = TestNameFilter(miniGame.ToString());
            e.transform.Find("TestResult").GetComponent<TextMeshProUGUI>().text = NumberOfCorrectAnswers + "/" + count;
        }
    }

    // Converts internal mini-game identifiers to user-friendly names
    string TestNameFilter(string v)
    {
        switch (v)
        {
            case "multi_step_problem":
                return "Word Problem";
            case "vertical_operations":
                return "Vertical Operation";
            case "find_compositions":
                return "Find Composition";
            default:
                return "";
        }
    }

    // Loads the main menu scene when the return button is clicked
    public void OnReturnTOMenuButtonClick()
    {
        Game.Instance.LoadScene(0);
    }
}
