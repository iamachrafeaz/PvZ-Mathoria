using System.Collections.Generic;
using System.Threading.Tasks;


// Responsible for loading test data from Firebase and constructing a TestComposition.
public class TestLoader
{
    // Default constructor
    public TestLoader(){}

    
    // Loads a test for the specified grade level from Firebase, builds and returns a TestComposition.
    // Returns null if no test is found or if the test is special and the player is not concerned.
    public async Task<TestComposition> LoadTestFromFirebase(string gradeLevel)
    {
        // Pull data from Firebase        
        var (testId, testData) = await FirebaseManager.Instance.GetTestForGrade(gradeLevel);

        // If no test data is found, return null
        if (testData == null)
        {
            return null;
        }

        // If the test is marked as special, check if the current player is concerned
        if (bool.Parse(testData["isSpecial"].ToString()))
        {
            if (!isSpecial(testData["concernedStudents"]))
            {
                return null;
            }
        }

        // Parse test metadata
        int TestDuration = int.Parse(testData["testDuration"].ToString());
        string teacherID = testData["teacherId"].ToString();
        string TestTitle = testData["testName"].ToString();

        TestFactory factory = new();

        // Create a new TestComposition with the loaded metadata
        TestComposition fullTest = new(TestDuration, testId, teacherID, TestTitle);

        // Get the order of mini-games and their configs
        var miniGameOrderDict = (List<object>)testData["miniGameOrder"];
        fullTest.miniGamesOrder = miniGameOrderDict;
        var testConfig = (Dictionary<string, object>)testData["miniGameConfigs"];

        // Add each mini-game to the test composition using the factory
        foreach (var item in miniGameOrderDict)
        {
            fullTest.Add(factory.GetTest(item.ToString(), (Dictionary<string, object>)testConfig[item.ToString()]));
        }

        return fullTest;
    }


    // Checks if the current player is among the concerned students for a special test.
    bool isSpecial(object S)
    {
        List<object> conernedStudents = (List<object>)S;
        return conernedStudents.Contains(Game.Instance.GetPlayer().getUid());
    }
}