using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageScore : MonoBehaviour
{
    public int score;
    public Text ScoreText;

    private MyFirebaseCounter myFirebaseCounter;

    // Start is called before the first frame update
    public void Start()
    {
        ScoreText.text = score.ToString();
        Debug.Log("Get reference to Firebase script");
        myFirebaseCounter = GetComponent<MyFirebaseCounter>();
        if (myFirebaseCounter==null)
        {
            Debug.Log("ERROR finding Firebase script");
        }
    }

    public void incrementScore()
    {
        score = score + 1;
        ScoreText.text = score.ToString();
        Debug.Log("Calling update in Firebase script");
        myFirebaseCounter.SaveScore(score);
        //firebaseCounter.UpdateFirebaseScore(score);
    }

    public void setScore(int newScore)
    {
        score = newScore;
        ScoreText.text = score.ToString();
        Debug.Log("Score updated externally");
    }

    public void SetUserID(string UserID)
    {
        Debug.Log("Updating the User for the Scores UserID = "+ UserID);
        myFirebaseCounter.SetUserID(UserID);
    }
}
