using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public static TestManager Instance;
    public TestComposition Tests;
    public bool UserHasAnswered { get; set; }
    public (int answer, bool isCorrect) PlayerAnswer { get; set; }
    string CurrentTestType;
    List<TestResult> TestResults;
    private bool hasTestStarted;

    // Singleton instance
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Don't destory this instance if the scene is changed.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        TestResults = new();
    }

    void Update()
    {
        // Start game's cycle when scene 2 (Test) is loaded
        if (SceneManager.GetActiveScene().buildIndex == 2 && !hasTestStarted)
        {
            hasTestStarted = true;
            StartCoroutine(RunSession());
        }
    }

    // Function to load configuration from firebase 
    public async Task<bool> GetTestConfig()
    {
        TestLoader LoadTest = new(); // Instanciate the test loader

        // Load the data using TestLoader and return a TestComposition instance
        Tests = await LoadTest.LoadTestFromFirebase(Game.Instance.GetPlayer().getSchoolGrade().ToString());

        if (Tests != null) // If the test composition is loaded
        {
            await FirebaseManager.Instance.SetTestSingleResultAsync(BuildTestData()); // Add test data to user in firebase
            return true;
        }
        else
        {
            return false; // If no test is loaded, return false
        }
    }

    // Function that run the cycle of the test
    private IEnumerator RunSession()
    {
        float totalDuration = Tests.TestDuration * 60f;
        int miniTestCount = Tests.GetTestsCount();
        float intervalBetweenTests = totalDuration / miniTestCount; // Devide test duration by the number of mini-test

        // For each mini-test, toggle between the test scene and the game scene
        for (int i = 0; i < miniTestCount; i++)
        {
            // For the first mini-test, don't pause the game, because the game hasn't been loaded yet
            if (i != 0)
            {
                GameManager.Instance.GetComponent<GameManager>().Pause(); // Pause the game to start a mini-test
                SceneManager.LoadScene(2, LoadSceneMode.Single); // Load the Test scene
            }


            // Each mini-test -> has a set of operation
            if (Tests.GetTestsCount() > 0)
            {
                UserHasAnswered = false; // This variable helps to know of the player has answered an operation or not yet

                TestManagerUI.Instance.OnTestStart(); // Show TestUI

                while (true)
                {
                    IOperation NextOpperation = Tests.Display(); // Get the next operation


                    if (TestManagerUI.Instance.operationCount == 0)
                    {
                        TestManagerUI.Instance.operationCount = Tests.GetElementsCount() + 1; // Set the number of operation for the current test. This help with progress bar
                    }

                    if (NextOpperation == null)
                    {
                        break; // if Tests.Display() returns null, which means that there are no operation left to show, break the while loop
                    }

                    CurrentTestType = Tests.GetTitle(); // Get the current Test type, ex : word_problem, etc..

                    // The following line executes a function that shows a specific TestUI + Operation based on the given arguments
                    TestManagerUI.Instance.CreateTestUI(CurrentTestType, NextOpperation);

                    yield return new WaitUntil(() => UserHasAnswered); // This line means -> don't do anything until the player solves the operation

                    yield return new WaitForSecondsRealtime(3f); // Wait 3s for a good user experience

                    // The following line adds operation's data + player's answer to the TestResult List which also send them to firebase for a realtime experience
                    TestResults.Add(new TestResult(CurrentTestType, NextOpperation.GetQuestion(), PlayerAnswer.answer.ToString(), PlayerAnswer.isCorrect, Tests.GetRequiredAnswers()));

                    // Set these attributs to their initial values
                    PlayerAnswer = (0, false);
                    UserHasAnswered = false;
                }

                _ = FirebaseManager.Instance.SetMiniGameResult(RunTestResult(), CurrentTestType); // For the current mini-test, send the result to firebase 

                int suns = TestManagerUI.Instance.GetWonSuns(); // Get the number of collected suns

                SceneManager.LoadScene(3, LoadSceneMode.Single); // Load the game scene

                TestManagerUI.Instance.CurrentTestUI.ActivateComponents(false); // Hide TestUI

                TestManagerUI.Instance.TogglePanel(false); // Hide Test Canvas

                yield return new WaitForSecondsRealtime(1); // Wait one second

                GameManager.Instance.GetComponent<GameManager>().setSuns(suns); // Set the number of collected suns in the game to be used by the player

                if (i != 0)
                {
                    GameManager.Instance.GetComponent<GameManager>().Continue(); // Continue the game because it has been paused it before
                }

            }
            yield return new WaitForSecondsRealtime(intervalBetweenTests); // Wait until it the next Test type time
        }

        // When the game time achives test duration, end session
        EndSession();
    }

    // Ends the game session: pauses the game, loads scene 4, triggers game finished event, then destroys this object.
 
    void EndSession()
    {
        GameManager.Instance.GetComponent<GameManager>().Pause();

        Game.Instance.LoadScene(4);

        GameManager.Instance.OnGameFinished();

        Destroy(gameObject);
    }


    Dictionary<string, object> BuildTestData()
    {
        DateTime now = DateTime.UtcNow;

        var miniGames = new Dictionary<string, object>();
        foreach (var game in Tests.miniGamesOrder)
            miniGames[game.ToString()] = "";

        return new Dictionary<string, object>
        {
            {
                Tests.TestID,
                new Dictionary<string, object>
                {
                    { "testID", Tests.TestID },
                    {"title" ,  Tests.TestTitle},
                    { "type", "soustraction" },
                    { "grade", Game.Instance.GetPlayer().getSchoolGrade() },
                    { "teacherID", Tests.TeacherID },
                    { "date" ,  now.ToString("o")},
                    { "miniGames", miniGames }
                }
            }
        };
    }
    
    // Function checks whether the player passed the current test and return true if so
    bool RunTestResult()
    {
        int correctAnswers = 0;
        int requiredCorrect = 0;
        int totalRelevant = 0;

        foreach (var result in TestResults)
        {
            if (result.testName == CurrentTestType)
            {
                totalRelevant++;
                requiredCorrect = result.req;
                if (result.isCorrect)
                    correctAnswers++;
            }
        }

        return correctAnswers >= requiredCorrect;
    }

    // Getter for TestResult attribut
    public List<TestResult> GetTestResult()
    {
        return TestResults;
    }
}
