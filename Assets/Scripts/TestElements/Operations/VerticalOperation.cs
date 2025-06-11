using System;

// Represents a single vertical operation (e.g., subtraction) for a mini-game.
// Implements the IOperation interface.

public class VerticalOperation : IOperation
{
    int min;                // Minimum value for operands
    int max;                // Maximum value for operands
    private bool withBorrowing;
    public int operandA;    // First operand (minuend)
    public int operandB;    // Second operand (subtrahend)

    Random rand;            // Random number generator

    bool IsAnswered;        // Tracks if the operation has been answered

    
    // Constructor: initializes the operation with operand bounds.
    
    public VerticalOperation(int min, int max, bool withBorrowing)
    {
        this.min = min;
        this.max = max;
        this.withBorrowing = withBorrowing;

        rand = new();

        IsAnswered = false;
    }

    
    // Generates operands for the operation, ensuring borrowing is needed (unitsB > unitsA).
    
    void GenerateOperands()
    {
        while (true)
        {
            operandA = rand.Next(min, max);      // e.g. 2-digit number
            operandB = rand.Next(min, operandA); // Ensure A > B

            int unitsA = operandA % 10;
            int unitsB = operandB % 10;

            // Borrowing needed if unitsB > unitsA
            if (unitsB > unitsA || !withBorrowing)
            {
                break;
            }
        }
    }

    
    // Generates the operation (calls GenerateOperands).
    
    public void GenerateOperation()
    {
        GenerateOperands();
    }

    
    // Returns whether this operation has been answered.
    
    public bool GetIsAnswered()
    {
        return IsAnswered;
    }

    
    // Sets the answered state for this operation.
    
    public void SetIsAnswered(bool IsAnswered)
    {
        this.IsAnswered = IsAnswered;
    }

    
    // Returns the question as a string (e.g., "23 - 7").
    
    public string GetQuestion()
    {
        return operandA + " - " + operandB;
    }
}