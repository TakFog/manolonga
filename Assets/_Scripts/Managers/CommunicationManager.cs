using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class CommunicationManager : MonoBehaviour
{
    public event Action<CommunicationData> OnMovesReceived;

    public float waitSeconds = 1f;
    private Coroutine _startCoroutine;
    public static CommunicationManager Instance;
    private bool hasChildSentRequest;
    private bool hasMonsterSentRequest;
    
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        
    }

    public void ResetRequests()
    {
        hasChildSentRequest = false;
        hasMonsterSentRequest = false;
    }
    public IEnumerator C_ClearServer()
    {
        var request =
            new UnityWebRequest(Globals.ServerAddress + "/clear", "GET");
        
        // Wait for the request to complete
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Successfully cleared");
        }
    }

    public void SubmitChoice(Choice move, PlayerType player, int round)
    {
        if (player == PlayerType.Child && !hasChildSentRequest)
            hasChildSentRequest = true;
        else if (player == PlayerType.Monster && !hasMonsterSentRequest)
            hasMonsterSentRequest = true;
        else
            return;
        _startCoroutine = StartCoroutine(C_Request(move, player, round));
    }

    IEnumerator C_Request(Choice choice, PlayerType player, int round)
    {
        // Create a new instance of the WebRequest class
        while (true)
        {
            var request = prepareRequest(choice, player, round);
            // Wait for the request to complete
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var movesOfThisRound = handleSuccess(request, round);
                if (movesOfThisRound.hasMonster && movesOfThisRound.hasChild)
                {
                    // Send event
                    OnMovesReceived?.Invoke(movesOfThisRound);
                    Debug.Log("Both moves are received: " + JsonUtility.ToJson(movesOfThisRound));
                    break;
                }

                // Wait for 1 second before sending the next request
                Debug.Log("Waiting for 1 second before sending the next request");
                yield return new WaitForSeconds(waitSeconds);
            }
            else
            {
                Debug.LogError("Error: " + request.error);
                yield return new WaitForSeconds(waitSeconds);
            }
        }
    }

    private UnityWebRequest prepareRequest(Choice choice, PlayerType player, int round)
    {
        var request =
            new UnityWebRequest(Globals.ServerAddress + "/updateState/" + player + "/" + round, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(choice)));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    private static CommunicationData handleSuccess(UnityWebRequest request, int round)
    {
        CommunicationData communicationsOfThisRound;
        var downloadHandlerText = request.downloadHandler.text;
        communicationsOfThisRound =
            JsonUtility.FromJson<CommunicationData>(downloadHandlerText);
        Debug.Log("Response: " + downloadHandlerText);
        Debug.Log("Monster: " + round + "/" + communicationsOfThisRound.Monster?.actionType + "/" + communicationsOfThisRound.Monster?.PositionsPath + "/" + communicationsOfThisRound.hasChild + "/" + communicationsOfThisRound.hasMonster);
        Debug.Log("Child: " + round + "/" + communicationsOfThisRound.Child?.actionType + "/" + communicationsOfThisRound.Child?.PositionsPath + "/" + communicationsOfThisRound.hasChild + "/" + communicationsOfThisRound.hasMonster);

        return communicationsOfThisRound;
    }
}