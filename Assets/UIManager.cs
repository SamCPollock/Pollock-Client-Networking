using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
     GameObject submitButton, userNameInput, passwordInput, createToggle, loginToggle;

     GameObject networkedClient;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in allObjects)
        {
            if (go.name == "UsernameInput")
                userNameInput = go;
            else if (go.name == "PasswordInput")
                passwordInput = go;
            else if (go.name == "CreateToggle")
                createToggle = go;
            else if (go.name == "LoginToggle")
                loginToggle = go;
            else if (go.name == "SubmitButton")
                submitButton = go;
            else if (go.name == "Client")
                networkedClient = go;
        }
        submitButton.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);

        loginToggle.GetComponent<Toggle>().onValueChanged.AddListener(LoginToggleChanged);
        createToggle.GetComponent<Toggle>().onValueChanged.AddListener(CreateToggleChanged);


    }

    public void SubmitButtonPressed()
    {
        string n = userNameInput.GetComponent<InputField>().text;
        string p = passwordInput.GetComponent<InputField>().text;

        string msg;
        if (createToggle.GetComponent<Toggle>().isOn)
            msg = ClientToServerSignifiers.CreateAccountAttempt + "," + n + "," + p;
        else
            msg = ClientToServerSignifiers.LoginAttempt + "," + n + "," + p;

        networkedClient.GetComponent<NetworkedClient>().SendMessageToHost(msg);
    }
    public void CreateToggleChanged(bool newValue)
    {
        loginToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void LoginToggleChanged(bool newValue)
    {
        createToggle.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);

    }

    // Update is called once per frame
    void Update()
    {

    }
}