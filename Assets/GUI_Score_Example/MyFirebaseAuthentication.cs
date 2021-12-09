using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class MyFirebaseAuthentication : MonoBehaviour
{
    private FirebaseAuth auth;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Setting up Firebase");
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        if (auth == null)
        {
            Debug.Log("ERROR -- Firebase Authentication setup FAILED");
        }
    }

    public void CreateUser (string email, string password)
    {
        // Code from https://firebase.google.com/docs/auth/unity/start
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    public void AuthenticateUser(string email, string password)
    {
        // Code from https://firebase.google.com/docs/auth/unity/start
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });

    }

    public void DisplayUser() 
    { 
        // Display the user info
        Firebase.Auth.FirebaseUser user = auth.CurrentUser;
        if (user != null)
        {
            string name = user.DisplayName;
            string email = user.Email;
            string uid = user.UserId;
        }

 
    }

    public void SignOutUser()
    {
        // sign out user
        auth.SignOut();
    }


}
