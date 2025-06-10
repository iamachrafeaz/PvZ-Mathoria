using UnityEngine;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase.Database;

public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Instance { get; private set; }

    public FirebaseDatabase Realtime { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        Realtime = FirebaseDatabase.DefaultInstance;
    }

    // Fetches a test from the Firebase Realtime Database for a specific grade, skipping drafts. Returns the test ID and its data as a dictionary.
    public async Task<(string testId, Dictionary<string, object> testData)> GetTestForGrade(string grade)
    {
        var reference = Realtime.GetReference("tests");


        var query = reference.OrderByChild("grade").EqualTo(grade);
        var snapshot = await query.GetValueAsync();

        if (!snapshot.Exists || !snapshot.HasChildren)
        {
            return (null, null);
        }

        foreach (var child in snapshot.Children)
        {
            var isDraft = child.Child("isDraft").Value?.ToString()?.ToLower() == "true";
            if (!isDraft)
            {
                var testId = child.Key;
                var testData = new Dictionary<string, object>();

                foreach (var item in child.Children)
                {
                    testData[item.Key] = item.Value;
                }

                return (testId, testData);
            }
        }

        return (null, null);
    }

    // Retrieves user data from Firebase for the given user ID. Returns the data as a dictionary or null if not found.
    public async Task<Dictionary<string, object>> GetPlayerData(string uid)
    {
        var snapshot = await Realtime
            .GetReference("users")
            .Child(uid)
            .GetValueAsync();

        if (!snapshot.Exists)
            return null;

        return snapshot.Value as Dictionary<string, object>;
    }

    // Saves or updates the overall test result for the current player in Firebase under their "tests" node.
    public async Task SetTestSingleResultAsync(Dictionary<string, object> result)
    {

        var docRef = Realtime.GetReference("users").Child(Game.Instance.GetPlayer().getUid()).Child("tests");
        await docRef.UpdateChildrenAsync(result);

    }

    // Stores the result of a single mini-game operation for the current test and player in Firebase, under the appropriate test and mini-game type.
    public async Task SetMiniGameResultPerOperation(Dictionary<string, object> result, string testTypeID)
    {
        string testID = TestManager.Instance.Tests.TestID;

        var docRef = Realtime.GetReference("users").Child(Game.Instance.GetPlayer().getUid()).Child("tests").Child(testID).Child("miniGames").Child(testTypeID).Push();

        await docRef.SetValueAsync(result);

    }

    // Stores the result of a single mini-game operation for the current test and player in Firebase, under the appropriate test and mini-game type.
    public async Task SetMiniGameResult(bool isPassed, string testTypeID)
    {
        string testID = TestManager.Instance.Tests.TestID;

        var docRef = Realtime.GetReference("users").Child(Game.Instance.GetPlayer().getUid()).Child("tests").Child(testID).Child("miniGames").Child(testTypeID).Child("isPassed");

        await docRef.SetValueAsync(isPassed);
    }
}
