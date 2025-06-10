
using UnityEngine;

public class FindCompositionOperation : IOperation
{

    int min;
    int max;
    public int operandA;
    public int operandB;
    public int correctAnswer;

    public int[] compositions;

    System.Random rand;

    bool IsAnswered;

    public FindCompositionOperation(int min, int max)
    {
        this.min = min;
        this.max = max;

        rand = new();

        compositions = new int[4];

        IsAnswered = false;
    }


    void GenerateOperands()
    {
        operandA = rand.Next(min, max);
        operandB = rand.Next(min, operandA);

        correctAnswer = operandA - operandB;
    }

    void GenerateCompositions()
    {
        compositions[0] = operandB;
        compositions[1] = operandB - 11;
        compositions[2] = operandB - 1;
        compositions[3] = operandB + 4;
        
        // Shuffle using Fisher-Yates algorithm
        for (int i = 3; i > 0; i--)
        {
            int j = rand.Next(0, i + 1); // Random index between 0 and i (inclusive)
                                         // Swap elements
            int temp = compositions[i];
            compositions[i] = compositions[j];
            compositions[j] = temp;
        }
    }




    public void GenerateOperation()
    {
        GenerateOperands();
        GenerateCompositions();
    }
    public bool GetIsAnswered()
    {
        return IsAnswered;
    }

    public void SetIsAnswered(bool IsAnswered)
    {
        this.IsAnswered = IsAnswered;
    }

    public string GetQuestion()
    {
        return operandA + " - ? (" + operandB + ") = " + correctAnswer;
    }
}