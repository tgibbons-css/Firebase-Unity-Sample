using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

// Technical Debt -- This class access Firebase and the Unity UI system.  Should only do one of those things
//                -- Need another class to break this up better into data model (Firebase) and controller or view (Unity UI)
public class ManageUserInfo : MonoBehaviour
{
    private FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;

    public ManageScore myManageScore;   // needed to tell the score display of user changes

    public Text userEmail;
    public Text userID;
    public Text userDisplayName;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Setting up Firebase");
        InitializeFirebase();
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("ManageUserInf0 -- AuthStateChanged");
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
                UpdateDisplay("Signed Out", " ", " ");
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                Debug.Log("Signed in " + user.UserId);
                UpdateDisplay(user.Email, user.UserId, user.DisplayName);
            }
        }
    }

    private void UpdateDisplay(string email, string userId, string displayName)
    {
        userEmail.text = email;
        userID.text = userId;
        userDisplayName.text = displayName;
        // update the score display with the new user
        myManageScore.SetUserID(userId);

    }
}
