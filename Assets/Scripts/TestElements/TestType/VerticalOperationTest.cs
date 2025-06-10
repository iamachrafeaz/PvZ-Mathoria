using System.Collections.Generic;


// Represents a vertical operation mini-game test (e.g., subtraction/addition questions).
// Generates a list of operations and manages their display and progress.
// Implements the ITest interface.

public class VerticalOperationTest : ITest
{
    // Test title
    string title;

    // Configuration for the vertical operation test
    int numOperations;                           // Number of questions in the test
    int minNumber;                               // Minimum operand value
    int maxNumber;                               // Maximum operand value
    int requiredCorrectAnswersMinumumPercentage; // Minimum correct answers required to pass

    bool withBorrowing;

    // List of generated vertical operations for this test
    Queue<VerticalOperation> operationsList;

    // The current operation being presented to the user
    VerticalOperation currentOperation;


    // Constructor: initializes the test with configuration and generates operations.

    public VerticalOperationTest(Dictionary<string, object> config)
    {
        title = "vertical_operations";
        minNumber = int.Parse(config["min_number"].ToString());
        maxNumber = int.Parse(config["max_number"].ToString());
        numOperations = int.Parse(config["num_questions"].ToString());
        requiredCorrectAnswersMinumumPercentage = int.Parse(config["min_required_answers"].ToString());
        operationsList = new Queue<VerticalOperation>();
        withBorrowing = bool.Parse(config["with_borrowing"].ToString());

        // Generate the list of operations for this test
        GenerateOperations();
    }


    // Generates the queue of vertical operations for the test.

    void GenerateOperations()
    {
        for (int i = 0; i < numOperations; i++)
        {
            VerticalOperation operation = new(minNumber, maxNumber, withBorrowing);
            operation.GenerateOperation();
            operationsList.Enqueue(operation);
        }
    }


    // Returns the next operation to display, or null if none remain.

    public IOperation Display()
    {
        if (numOperations == 0 || GetElementsCount() == 0)
        {
            return null;
        }

        currentOperation = operationsList.Dequeue();

        return currentOperation;
    }


    // Returns the number of operations left in the test.

    public int GetElementsCount()
    {
        return operationsList.Count;
    }


    // Returns the title of this test type.

    public string GetTitle()
    {
        return title;
    }


    // Returns the minimum number of correct answers required to pass.

    public int GetRequiredAnswers()
    {
        return requiredCorrectAnswersMinumumPercentage;
    }
}