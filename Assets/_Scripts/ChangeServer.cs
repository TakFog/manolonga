using TMPro;
using UnityEngine;

public class ChangeServer : MonoBehaviour
{
    public TMP_InputField serverAddressInputField;
    
    public void ChangeServerAddress(string serverAddress)
    {
        Debug.Log("Changing server address to: " + serverAddress);
        Globals.ServerAddress = serverAddress;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        serverAddressInputField.text = Globals.ServerAddress;
        serverAddressInputField.onValueChanged.AddListener(ChangeServerAddress);
    }
}
