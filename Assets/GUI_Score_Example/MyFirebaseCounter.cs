using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;

public class MyFirebaseCounter : MonoBehaviour
{

    public DatabaseReference DbReference;
    private ManageScore manageScoreScript;
    string UserID;

    // Start is called before the first frame update
    void Start()
    {
        UserID = "Anonymous";
        InitFirebase();
        manageScoreScript = GetComponent<ManageScore>();
        StartCoroutine(LoadFirebase());    // must be called with StartCoroutine
    }

    private void InitFirebase()
    {
        Debug.Log("Setting up Firebase");
        DbReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private IEnumerator LoadFirebase()
    {
        int updatedScore = 0;
        string strScore = "-1";
        var DBTask = DbReference.Child("users").Child(UserID).GetValueAsync();
        // wait for data to be returned
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.IsCompleted)
        {
            DataSnapshot snapshot = DBTask.Result;
            if (snapshot.Exists)
            {
                strScore = snapshot.Child("score").Value.ToString();
            }
            updatedScore = int.Parse(strScore);
        }
        updateScoreDisplay(updatedScore);
    }

    private IEnumerator UpdateFirebase(int newScore)
    {
        Debug.Log("Attempt to update Firebase database with new score");

        // create a task to update the score field in the firebase database
        var DBTask = DbReference.Child("users").Child(UserID).Child("score").SetValueAsync(newScore);

        // wait for update to complete
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        // Check for errors with the update -- this is actually optional
        if (DBTask.Exception != null)
        {
            Debug.Log("DBTask UpdateFirebase ERROR");
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("Firebase score updated to " + newScore);
        }
    }

    private void updateScoreDisplay(int updatedScore)
    {
        manageScoreScript.setScore(updatedScore);

    }

    public void SetUserID(string newUserID)
    {
        updateScoreDisplay(-1);     // Set score to holding value
        UserID = newUserID;
        StartCoroutine(LoadFirebase());    // must be called with StartCoroutine
    }

    public void SaveScore(int newScore)
    {
        Debug.Log("SaveScore with new score = " + newScore);
        //UpdateFirebase(newScore);    ==== must be called with StartCoRoutine
        StartCoroutine(UpdateFirebase(newScore));
    }

}
