using System.Linq;
using TMPro;
using UnityEngine;


// Handles the UI for vertical operation mini-games (e.g., subtraction),
// including displaying operands, collecting user input, and checking answers.
// Implements ITestChecker and ITestUI.

public class VerticalOperationUI : MonoBehaviour, ITestChecker, ITestUI
{
    [SerializeField] GameObject PickedNumbers;      // Container for answer input fields
    [SerializeField] GameObject FirstRow;           // Container for first operand digits
    [SerializeField] GameObject SecondRow;          // Container for second operand digits
    [SerializeField] GameObject BorrowingContainer; // Container for borrowing
    [SerializeField] GameObject numberPrefab;       // Prefab for displaying a digit
    [SerializeField] GameObject inputPrefab;        // Prefab for inputting a digit
    [SerializeField] GameObject Keyboard; // On-screen keyboard UI

    private int CorrectAnswer;            // The correct answer for the operation
    int PlayerAnswer;                     // The player's answer

    VerticalOperation CurrentOperation;   // The current operation data

    
    // Checks the user's answer against the correct answer.
    // Returns a tuple: (isCorrect, playerAnswer).    
    public (bool, int) CheckAnswer()
    {
        PlayerAnswer = 0;

        int index = PickedNumbers.transform.childCount - 1;
        foreach (Transform input in PickedNumbers.transform)
        {
            string text = input.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text;

            if (!text.Equals(""))
            {
                PlayerAnswer += int.Parse(text) * (int)Mathf.Pow(10, index--);
            }
            else
            {
                PlayerAnswer += 0 * (int)Mathf.Pow(10, index++);
            }
        }

        if (PlayerAnswer == CorrectAnswer)
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
        CurrentOperation = (VerticalOperation)operation;

        int[] fN = IntToArr(CurrentOperation.operandA);
        GenerateNumbersUI(fN, FirstRow, false);

        int[] sN = IntToArr(CurrentOperation.operandB);
        GenerateNumbersUI(sN, SecondRow, false);

        CorrectAnswer = CurrentOperation.operandA - CurrentOperation.operandB;

        int[] aN = IntToArr(CorrectAnswer);

        GenerateNumbersUI(aN, PickedNumbers, true);

        ActivateComponents(true);
    }

    
    // Activates or deactivates the main components of the UI.
    public void ActivateComponents(bool state)
    {
        Keyboard.SetActive(state);
        gameObject.SetActive(state);
    }

    
    // Generates UI elements for a number (either as static digits or input fields).
    void GenerateNumbersUI(int[] ArrayedNumber, GameObject Row, bool isInput)
    {
        for (int i = 0; i < ArrayedNumber.Length; i++)
        {
            if (isInput)
            {
                Instantiate(inputPrefab, Row.transform);
            }
            else
            {
                GameObject number = Instantiate(numberPrefab, Row.transform);
                number.transform.GetComponent<TextMeshProUGUI>().text = ArrayedNumber[i].ToString();
            }
        }
    }

    
    // Converts an integer to an array of its digits. 
    int[] IntToArr(int i)
    {
        return i.ToString().Select(c => int.Parse(c.ToString())).ToArray();
    }


    // Clears all input and number fields from the UI.  
    public void Clean()
    {
        DeleteAllChildren(FirstRow);
        DeleteAllChildren(SecondRow);
        DeleteAllChildren(PickedNumbers);
        BorrowingContainer.transform.Find("InputText").GetComponent<TextMeshProUGUI>().text = "";
    }

    
    // Deletes all child GameObjects from a parent GameObject.
    
    void DeleteAllChildren(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
