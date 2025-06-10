using TMPro;
using UnityEngine;


// Handles the UI for word problem mini-games, including displaying the problem,
// collecting user input, and checking answers. Implements ITestChecker and ITestUI.

public class WordProblemUI : MonoBehaviour, ITestChecker, ITestUI
{
    [SerializeField] GameObject Problem;         // UI element for the problem/question text
    [SerializeField] GameObject[] FirstRow;      // UI input fields for the first row (if needed)
    [SerializeField] GameObject[] SecondRow;     // UI input fields for the second row (if needed)
    [SerializeField] GameObject[] ResultRow;     // UI input fields for the answer/result
    [SerializeField] GameObject BorrowingContainer; // Container for borrowing

    [SerializeField] GameObject Keyboard;        // On-screen keyboard UI
    [SerializeField] GameObject PromptText;      // UI element for prompt/instructions

    WordProblemOperation currentOperation;       // The current word problem operation
    int PlayerAnswer;                            // The player's answer as an integer


    // Activates or deactivates the main components of the UI.
    public void ActivateComponents(bool state)
    {
        gameObject.SetActive(state);
        Keyboard.SetActive(state);
    }


    // Populates the UI with the given operation's data.

    public void OperationToUI(IOperation operation)
    {
        Clean();

        currentOperation = (WordProblemOperation)operation;

        setPromptText(currentOperation.description);

        Problem.transform.GetComponent<TextMeshProUGUI>().text = currentOperation.question;

        ActivateComponents(true);
    }


    // Clears all input fields in the UI.

    public void Clean()
    {
        foreach (GameObject input in FirstRow)
        {
            input.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
        }

        foreach (GameObject input in SecondRow)
        {
            input.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
        }

        foreach (GameObject input in ResultRow)
        {
            input.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
        }

        BorrowingContainer.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
    }


    // Checks the user's answer against the correct answer.
    // Returns a tuple: (isCorrect, playerAnswer).

    public (bool, int) CheckAnswer()
    {
        PlayerAnswer = 0;

        int index = 0;
        foreach (GameObject input in ResultRow)
        {
            string tempVal = input.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text;

            if (!tempVal.Equals(""))
            {
                PlayerAnswer += int.Parse(tempVal) * (int)Mathf.Pow(10, index++);
            }
            else
            {
                PlayerAnswer += 0 * (int)Mathf.Pow(10, index++);
            }
        }
        return PlayerAnswer == currentOperation.correctAnswer ? (true, PlayerAnswer) : (false, PlayerAnswer);
    }


    // Sets the prompt/instruction text in the UI.

    public void setPromptText(string prompt)
    {
        PromptText.transform.GetComponent<TextMeshProUGUI>().text = prompt;
    }
}
