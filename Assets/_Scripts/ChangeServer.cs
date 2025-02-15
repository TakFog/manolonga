using System.Collections;
using TMPro;
using UnityEngine;

public class ChangeServer : MonoBehaviour
{
    public TMP_InputField serverAddressInputField;
    
    public void ChangeServerAddress(string serverAddress)
    {
        if (Globals.DefaultServerAddress != null && !serverAddress.Contains("."))
            serverAddress = Globals.DefaultServerAddress + "/" + serverAddress.ToUpper();
        Debug.Log("Changing server address to: " + serverAddress);
        Globals.ServerAddress = serverAddress;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        serverAddressInputField.text = Globals.ServerAddress;
        serverAddressInputField.onValueChanged.AddListener(ChangeServerAddress);
    }

    private void OnEnable()
    {
        if (serverAddressInputField.text.Length == 0)
        {
            if (CommunicationManager.Instance != null) InitGameServer();
            else StartCoroutine(C_InitGameServer());
        }
    }

    IEnumerator C_InitGameServer()
    {
        while (true)
        {
            if (CommunicationManager.Instance != null)
            {
                InitGameServer();
                break;
            }
            else yield return null;
        }
    }

    private void InitGameServer()
    {
        if (serverAddressInputField.text.Length == 0)
        {
            CommunicationManager.Instance.OnGameIdReceived += SetGameId;
            StartCoroutine(CommunicationManager.Instance.C_RequestGameId());
        }
    }

    void OnDisable()
    {
        CommunicationManager.Instance.OnGameIdReceived -= SetGameId;
    }

    private void SetGameId(string gameId)
    {
        if (serverAddressInputField.text == "")
        {
            Globals.ServerAddress = Globals.DefaultServerAddress + "/" + gameId;
            serverAddressInputField.text = gameId;
        }
    }
}
