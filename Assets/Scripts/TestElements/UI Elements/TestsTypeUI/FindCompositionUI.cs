using TMPro;
using UnityEngine;


// Handles the UI for find composition mini-games,
// including displaying operands, collecting user input, and checking answers.
// Implements ITestChecker and ITestUI.

public class FindCompositionUI : MonoBehaviour, ITestChecker, ITestUI
{
    [SerializeField] GameObject Compositions; // Container for possible composition options
    [SerializeField] GameObject OperandA;     // UI element for operand A
    [SerializeField] GameObject OperandB;     // UI input field for operand B (user input)
    [SerializeField] GameObject Result;       // UI element for the result

    int PlayerAnswer;                         // The player's answer

    FindCompositionOperation CurrentOperation; // The current operation data


    // Checks the user's answer against the correct answer.
    // Returns a tuple: (isCorrect, playerAnswer).

    public (bool, int) CheckAnswer()
    {
        PlayerAnswer = 0;

        string tempVal = OperandB.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text;

        if (int.TryParse(tempVal, out int digit))
        {
            PlayerAnswer = digit;
        }

        if (PlayerAnswer == CurrentOperation.operandB)
        {
            return (true, PlayerAnswer);
        }
        else
        {
            return (false, PlayerAnswer);
        }
    }


    // Populates the UI with the given operation's data.

    public void OperationToUI(IOperation operation)
    {
        Clean();

        CurrentOperation = (FindCompositionOperation)operation;

        OperandA.transform.GetComponent<TextMeshProUGUI>().text = CurrentOperation.operandA.ToString();

        Result.transform.GetComponent<TextMeshProUGUI>().text = CurrentOperation.correctAnswer.ToString();

        ActivateComponents(true);

        FillCompositionCubes();
    }


    // Randomly fills the composition options, ensuring the correct answer is present.

    void FillCompositionCubes()
    {
        int count = 0;
        foreach (Transform composition in Compositions.transform)
        {
            Transform tmp = composition;
            tmp.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = CurrentOperation.compositions[count].ToString();
            count++;
        }
    }


    // Activates or deactivates the main components of the UI.

    public void ActivateComponents(bool state)
    {
        Compositions.SetActive(state);
        gameObject.SetActive(state);
    }


    // Clears all input and number fields from the UI.

    public void Clean()
    {
        OperandA.transform.GetComponent<TextMeshProUGUI>().text = "";
        OperandB.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
        Result.transform.GetComponent<TextMeshProUGUI>().text = "";
    }
}