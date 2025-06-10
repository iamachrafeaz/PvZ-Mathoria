using System.Collections.Generic;
using UnityEngine;

class TestFactory : MonoBehaviour
{
    public ITest GetTest(string testID, Dictionary<string, object> config)
    {
        switch (testID)
        {
            case "find_compositions":
                return new FindCompositionTest(config);
            case "vertical_operations":
                return new VerticalOperationTest(config);
            case "multi_step_problem":
                return new MultiStepWordProblemTest(config);
            default:
                return null;
        }
    }
}