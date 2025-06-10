using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages login UI, state transitions, and player test setup.

public class LoginManager : MonoBehaviour
{
    public static LoginManager Instance;

    [SerializeField] GameObject StartButton;
    [SerializeField] GameObject LoginButton;
    [SerializeField] GameObject Responce;
    [SerializeField] GameObject LoadingCircle;
    [SerializeField] GameObject HelloText;
    [SerializeField] GameObject TestManagerScript;
    [SerializeField] GameObject TestState;
    [SerializeField] AudioSource ClickedButtonAudio;

    // Initializes singleton instance and sets up initial UI state.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Avoid duplicate instances
        }

        // Set initial UI visibility
        TestManagerScript.SetActive(false);
        StartButton.SetActive(false);
        LoginButton.SetActive(true);
        Responce.SetActive(false);
        LoadingCircle.SetActive(false);
        HelloText.SetActive(false);
        TestState.SetActive(false);
    }

    // Sets up listener for the Start button to launch the game.
    void Start()
    {
        StartButton.GetComponent<Button>().onClick.AddListener(async () =>
        {
            await OnLaunchButtonClick();
        });
    }

    // Handles UI updates each frame depending on login state and current scene.
    void Update()
    {
        ToggleCanvas();

        if (Game.Instance.GetPlayer() == null)
        {
            // Player not logged in
            LoginButton.SetActive(true);
        }
        else
        {
            // Player is logged in
            LoginButton.SetActive(false);
            StartButton.SetActive(true);
            GreetPlayer();
            HelloText.SetActive(true);
        }
    }

    // Called when the login button is clicked. Plays sound and transitions to QR scan scene.
    public void OnLoginButtonClick()
    {
        LoginButton.SetActive(false);
        StartCoroutine(PlaySoundAndLoadScene());
    }

    // Plays click sound then loads QR scanner scene after a delay.
    private IEnumerator PlaySoundAndLoadScene()
    {
        ClickedButtonAudio.Play();
        yield return new WaitForSeconds(1f);
        Game.Instance.LoadScene(1); // Load QR scanner scene
    }


    // Displays greeting message with the player's full name.
    void GreetPlayer()
    {
        HelloText.GetComponent<TextMeshProUGUI>().text = "Hello, " + Game.Instance.GetPlayer().getFullName();
    }


    // Launches the test setup flow and transitions to gameplay scene if ready.
    public async Task OnLaunchButtonClick()
    {
        ClickedButtonAudio.Play();

        TestState.SetActive(false);

        // Show loading message
        Responce.GetComponent<TextMeshProUGUI>().text = "Loading game data, please wait...";
        Responce.SetActive(true);
        LoadingCircle.SetActive(true);

        TestManagerScript.SetActive(true);

        // Attempt to load test config
        bool isTestReady = await TestManager.Instance.GetTestConfig();

        // Hide loading visuals
        Responce.SetActive(false);
        LoadingCircle.SetActive(false);

        if (!isTestReady)
        {
            // No test found
            TestState.GetComponent<TextMeshProUGUI>().text = "No test found. Try again";
            TestState.SetActive(true);
        }
        else
        {
            // Test ready, proceed
            TestState.GetComponent<TextMeshProUGUI>().text = "Game is ready.";
            TestState.SetActive(true);

            Game.Instance.LoadScene(2); // Load test/game scene

            WaitDelay(2f); // Dummy coroutine, no yield
            Destroy(gameObject); // Clean up LoginManager
        }
    }


    // Enables or disables the UI canvas based on current scene index.

    void ToggleCanvas()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Show canvas in login/menu scene
            transform.Find("Canvas").gameObject.SetActive(true);
        }
        else
        {
            // Hide canvas in all other scenes
            transform.Find("Canvas").gameObject.SetActive(false);
        }
    }

    // Coroutine that waits for a given delay. Currently not awaited or used effectively.

    IEnumerator WaitDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
