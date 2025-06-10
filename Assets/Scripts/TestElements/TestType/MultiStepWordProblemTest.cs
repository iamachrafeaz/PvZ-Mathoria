using System.Collections.Generic;


// Represents a multi-step word problem test, where each step is a word problem.
// Implements the ITest interface.

public class MultiStepWordProblemTest : ITest
{
    // Test title
    string title;
    // Description or prompt for the test
    string description;

    // Number of steps/questions in the test
    int numSteps;

    // Minimum correct answers required to pass
    int requiredCorrectAnswersMinumumPercentage;

    // Queue of word problem operations (steps)
    Queue<WordProblemOperation> stepList;

    // The current operation being presented to the user
    WordProblemOperation currentOperation;

    
    // Constructor: initializes the test with configuration and generates problems.
    
    public MultiStepWordProblemTest(Dictionary<string, object> config)
    {
        title = "multi_step_problem";
        numSteps = int.Parse(config["num_questions"].ToString());
        description = config["prompt_text"].ToString();
        requiredCorrectAnswersMinumumPercentage = int.Parse(config["min_required_answers"].ToString());
        stepList = new Queue<WordProblemOperation>();

        // Generate operations from the provided steps
        GenerateProblems((List<object>)config["steps"]);
    }

    
    // Generates the queue of word problem operations for the test.
    
    void GenerateProblems(List<object> sL)
    {
        foreach (Dictionary<string, object> item in sL)
        {
            stepList.Enqueue(new WordProblemOperation(description, item["question"].ToString(), int.Parse(item["answer"].ToString())));
        }
    }

    
    // Returns the next operation to display, or null if none remain.
    
    public IOperation Display()
    {
        if (numSteps == 0 || GetElementsCount() == 0)
        {
            return null;
        }

        currentOperation = stepList.Dequeue();

        return currentOperation;
    }

    
    // Returns the number of operations left in the test.
    
    public int GetElementsCount()
    {
        return stepList.Count;
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

