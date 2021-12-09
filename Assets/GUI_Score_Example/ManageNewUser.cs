using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageNewUser : MonoBehaviour
{
    public InputField userNameField;
    public InputField passwordField;

    private string email;
    private string password;

    private MyFirebaseAuthentication myAuthScript;

    // Start is called before the first frame update
    void Start()
    {
        myAuthScript = GetComponent<MyFirebaseAuthentication>();
    }

    public void CreateNewUser()
    {
        email = userNameField.text;
        password = passwordField.text;
        Debug.Log("New username is " + email + " and password is " + password);
        myAuthScript.CreateUser(email, password);
    }
}
