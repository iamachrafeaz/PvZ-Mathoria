using System.Collections.Generic;

public class FindCompositionTest : ITest
{
    // Attributs 
    string title;

    int numCompositions;
    int minNumber;
    int maxNumber;
    int requiredCorrectAnswersMinumumPercentage;

    Queue<FindCompositionOperation> operationsList;
    FindCompositionOperation currentOperation;


    //Constructor
    public FindCompositionTest(Dictionary<string, object> config)
    {
        title = "find_compositions";

        numCompositions = int.Parse(config["num_questions"].ToString());
        minNumber = int.Parse(config["min_number"].ToString());
        maxNumber = int.Parse(config["max_number"].ToString());
        requiredCorrectAnswersMinumumPercentage = int.Parse(config["min_required_answers"].ToString());

        operationsList = new Queue<FindCompositionOperation>();

        // Generate operations
        GenerateOperations();
    }

    // Methods
    void GenerateOperations()
    {
        for (int i = 0; i < numCompositions; i++)
        {
            FindCompositionOperation operation = new(minNumber, maxNumber);
            operation.GenerateOperation();
            operationsList.Enqueue(operation);
        }
    }

    public IOperation Display()
    {
        if (numCompositions == 0 || GetElementsCount() == 0)
        {
            return null;
        }

        currentOperation = operationsList.Dequeue();

        return currentOperation;
    }


    public int GetElementsCount()
    {
        return operationsList.Count;
    }
    public int GetRequiredAnswers()
    {
        return requiredCorrectAnswersMinumumPercentage;
    }
    public string GetTitle()
    {
        return title;
    }
}