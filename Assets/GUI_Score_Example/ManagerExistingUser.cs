using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerExistingUser : MonoBehaviour
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

    public void LoginUser()
    {
        email = userNameField.text;
        password = passwordField.text;
        Debug.Log("Existing email is " + email + " and password is " + password);
        myAuthScript.AuthenticateUser(email, password);
    }

    public void SignOutUser()
    {
        myAuthScript.SignOutUser();
    }
}
