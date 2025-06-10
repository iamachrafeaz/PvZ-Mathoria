using System.Collections.Generic;

// Represents a composition of multiple mini-game tests, managing their order and execution.
// Implements the ITest interface.
public class TestComposition : ITest
{
    // Duration of the test in seconds
    public int TestDuration { get; }
    // Unique identifier for the test
    public string TestID;
    // Teacher's identifier
    public string TeacherID;
    // Title of the test
    public string TestTitle;

    // Order of mini-games as defined in the test data
    public List<object> miniGamesOrder;
    // Queue of mini-game test instances to be executed
    public Queue<ITest> tests;
    // The currently active mini-game test
    private ITest currentTest;

    // Constructor: initializes the test composition with metadata.
    public TestComposition(int tD, string tID, string teID, string teTit)
    {
        TestID = tID;
        TestDuration = tD;
        TeacherID = teID;
        TestTitle = teTit;

        tests = new Queue<ITest>();
    }


    // Adds a mini-game test to the composition.
    public void Add(ITest test)
    {
        tests.Enqueue(test);
    }


    // Displays the current mini-game test, or advances to the next if needed.
    // Returns null if there are no more tests.
    public IOperation Display()
    {
        if (currentTest == null)
        {
            currentTest = tests.Dequeue();
        }

        if (GetElementsCount() == 0)
        {
            currentTest = null;
            return null;
        }

        return currentTest.Display();
    }


    // Gets the number of elements/questions left in the current mini-game test.

    public int GetElementsCount()
    {
        if (currentTest == null)
            return 0;
        return currentTest.GetElementsCount();
    }


    // Gets the number of remaining mini-game tests in the composition.

    public int GetTestsCount()
    {
        return tests.Count;
    }


    // Gets the title of the current mini-game test.

    public string GetTitle()
    {
        return currentTest.GetTitle();
    }


    // Gets the required number of correct answers for the current mini-game test.

    public int GetRequiredAnswers()
    {
        return currentTest.GetRequiredAnswers();
    }
}