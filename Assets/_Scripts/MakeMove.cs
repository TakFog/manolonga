using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class MakeMove : MonoBehaviour
{
    public PlayerType player;
    public float waitSeconds = 1f;
    public Move moveObjectForTest; // TODO Remove
    private Coroutine _startCoroutine;

    public void MoveInsert(Move move)
    {
        if (_startCoroutine == null)
        {
            _startCoroutine = StartCoroutine(Request(move));
        }
    }

    public void MoveTest()
    {
        if (moveObjectForTest != null)
        {
            MoveInsert(moveObjectForTest);
        }
    }

    IEnumerator Request(Move move)
    {
        // Create a new instance of the WebRequest class
        while (true)
        {
            var request = prepareRequest(move);
            // Wait for the request to complete
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var movesOfThisRound = handleSuccess(request);
                if (movesOfThisRound.Count >= 2)
                {
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

    private UnityWebRequest prepareRequest(Move move)
    {
        var request = new UnityWebRequest("http://localhost:8080/updateState/" + player + "/" + move.round, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(move)));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        return request;
    }

    private static Dictionary<string, Move> handleSuccess(UnityWebRequest request)
    {
        Dictionary<string, Move> movesOfThisRound;
        var downloadHandlerText = request.downloadHandler.text;
        movesOfThisRound =
            JsonUtility.FromJson<Dictionary<String, Move>>(downloadHandlerText);
        Debug.Log("Response: " + downloadHandlerText);

        foreach (var fromJsonKey in movesOfThisRound.Keys)
        {
            var s = movesOfThisRound[fromJsonKey].player;
            var i = movesOfThisRound[fromJsonKey].round;

            Debug.Log("Key: " + fromJsonKey + " - " + s + " - " + i);
        }

        return movesOfThisRound;
    }

    [Serializable]
    public class Move
    {
        public PlayerType player;
        public int round;
        public Vector2 position;
    }

    public enum PlayerType
    {
        Child,
        Monster,
    }
}