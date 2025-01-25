using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SendPayload : MonoBehaviour
{
    public string player;

    public void SendPayloadToServer(int round)
    {
        // Wait for the request to complete
        StartCoroutine(Request(round));
    }

    IEnumerator Request(int round)
    {
        // Create a new instance of the WebRequest class
        var request = new UnityWebRequest("http://localhost:8080/updateState/" + player + "/" + round, "POST");

        var payload = new Payload();
        payload.player = player;
        payload.round = round;

        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(JsonUtility.ToJson(payload)));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        // Wait for the request to complete
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var downloadHandlerText = request.downloadHandler.text;
            var fromJson = JsonUtility.FromJson<MyDictionary>(downloadHandlerText);
            Debug.Log("Response: " + downloadHandlerText);
            
                var valueOfPlayer = fromJson.monster;
                var deserialzied = JsonUtility.FromJson<Payload>(valueOfPlayer);
                Debug.Log("monster: " + deserialzied.player + " - " + deserialzied.round);
            
        }
        else
        {
            Debug.LogError("Error: " + request.error);
        }
    }
    
    [System.Serializable]
    public class Payload
    {
        public string player;
        public int round;
    }
    
    [System.Serializable]
    public class MyDictionary 
    {
        public string monster;
        public string child;
    }
}