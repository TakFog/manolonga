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
    private bool coroutineIsRunning = false;
    
    private void Awake()
    {
        Instance = this;
    }

    public void SubmitChoice(Choice move, PlayerType player)
    {
        if (_startCoroutine == null || !coroutineIsRunning)
        {
            _startCoroutine = StartCoroutine(Request(move, player));
        }
    }

    IEnumerator Request(Choice choice, PlayerType player)
    {
        coroutineIsRunning = true;
        // Create a new instance of the WebRequest class
        while (true)
        {
            var request = prepareRequest(choice, player);
            // Wait for the request to complete
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var movesOfThisRound = handleSuccess(request);
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

        coroutineIsRunning = false;
    }

    private UnityWebRequest prepareRequest(Choice choice, PlayerType player)
    {
        var request = new UnityWebRequest("http://localhost:8080/updateState/" + player + "/" + choice.Round, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(choice)));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    private static CommunicationData handleSuccess(UnityWebRequest request)
    {
        CommunicationData communicationsOfThisRound;
        var downloadHandlerText = request.downloadHandler.text;
        communicationsOfThisRound =
            JsonUtility.FromJson<CommunicationData>(downloadHandlerText);
        Debug.Log("Response: " + downloadHandlerText);
        Debug.Log("Monster: " + communicationsOfThisRound.MonsterChoice?.Round + "/" + communicationsOfThisRound.MonsterChoice?.actionType + "/" + communicationsOfThisRound.MonsterChoice?.PositionsPath + "/" + communicationsOfThisRound.hasChild + "/" + communicationsOfThisRound.hasMonster);
        Debug.Log("Child: " + communicationsOfThisRound.ChildChoice?.Round + "/" + communicationsOfThisRound.ChildChoice?.actionType + "/" + communicationsOfThisRound.ChildChoice?.PositionsPath + "/" + communicationsOfThisRound.hasChild + "/" + communicationsOfThisRound.hasMonster);

        return communicationsOfThisRound;
    }
}

public class TESTCommunicationManager : MonoBehaviour
{
    public static TESTCommunicationManager Instance;
    public PlayerType player;
    public Choice choice; // TODO Remove

    private void Awake()
    {
        Instance = this;
    }


    public static void SendFakeChoice()
    {
        CommunicationManager.Instance.SubmitChoice(Instance.choice, Instance.player);
    }
}