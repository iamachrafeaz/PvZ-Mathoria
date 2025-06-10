using System.Collections.Generic;
using System.Threading.Tasks;


// Represents the result of a single test question/operation,
// and automatically sends the result to the database upon creation.

public class TestResult
{
    // The question text
    public string question;
    // The user's answer
    public string userAnswer;
    // Whether the answer was correct
    public bool isCorrect;
    // The name/type of the test this result belongs to
    public string testName;
    // The required value for this test (e.g., minimum correct answers)
    public int req;

    
    // Constructor: initializes the result and sends it to the database.
    
    public TestResult(string testName, string question, string userAnswer, bool i, int req)
    {
        this.testName = testName;
        this.req = req;
        this.question = question;
        this.userAnswer = userAnswer;
        isCorrect = i;

        // Immediately send this result to the database asynchronously
        _ = SendToDatabase();
    }

    
    // Sends this test result to Firebase using the FirebaseManager.
    async Task SendToDatabase()
    {
        Dictionary<string, object> operation = new();

        operation["question"] = question;
        operation["answer"] = userAnswer;
        operation["isCorrect"] = isCorrect;

        await FirebaseManager.Instance.SetMiniGameResultPerOperation(operation, testName);
    }
}