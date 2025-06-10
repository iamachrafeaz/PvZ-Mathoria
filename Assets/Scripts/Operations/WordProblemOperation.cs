
// Represents a single word problem operation/question for a mini-game.
// Implements the IOperation interface.

public class WordProblemOperation : IOperation
{
    public string description;      // Description or prompt for the word problem
    public string question;         // The actual question text
    public int correctAnswer;       // The correct answer to the problem

    public int[] compositions;      // (Optional) Possible answer compositions

    bool IsAnswered;                // Tracks if the operation has been answered

    
    // Constructor: initializes the word problem operation with its data.
    
    public WordProblemOperation(string description, string question, int answer)
    {
        this.question = question;
        this.correctAnswer = answer;
        this.description = description;
        IsAnswered = false;
    }

    
    // (Unused) Generates the operation. Included for interface compatibility.
    
    public void GenerateOperation(){}

    
    // Returns whether this operation has been answered.
    
    public bool GetIsAnswered()
    {
        return IsAnswered;
    }

    
    // Returns the full question string (description + question).
    
    public string GetQuestion()
    {
        return description + " --> " + question;
    }

    
    // Sets the answered state for this operation.
    
    public void SetIsAnswered(bool IsAnswered)
    {
        this.IsAnswered = IsAnswered;
    }
}