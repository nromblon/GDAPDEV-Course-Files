using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.UIElements;
using System.Net;

public class WebHandlerSample : MonoBehaviour
{
    public string BaseURL
    {
        get
        {
            return "https://gdapdev-web-api.herokuapp.com/api/";
        }
    }

    private void Start()
    {
    }

    public void CreatePlayer()
    {
        StartCoroutine(SamplePostRequest());
    }

    IEnumerator SamplePostRequest()
    {
        Dictionary<string, string> playerParams = new Dictionary<string, string>();

        playerParams.Add("nickname", "Bob2");
        playerParams.Add("name", "Bob Bobson");
        playerParams.Add("email", "bob@bobmail.com");
        playerParams.Add("contact", "09091234123");

        // Turns the dictionary into a JSON string
        string requestString = JsonConvert.SerializeObject(playerParams);
        // Convert the string into bytes
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);

        Debug.Log(requestString);

        // Create a POST request directed to the /players route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "POST"))
        {
            // Set what type of data is in the request
            request.SetRequestHeader("Content-Type", "application/json");
            // Add the request data using UploadHandler
            request.uploadHandler = new UploadHandlerRaw(requestData);
            // Create a receiver for the response later
            request.downloadHandler = new DownloadHandlerBuffer();
            Debug.Log("Sending post request...");
            yield return request.SendWebRequest();
            Debug.Log($"POST.response code: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error)) 
            {
                Debug.Log($"POST success: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"POST error: {request.error}");
            }
        }
        playerParams.Clear();
    }

    public void GetPlayers()
    {
        StartCoroutine(SampleGetPlayersRequest());
    }

    IEnumerator SampleGetPlayersRequest()
    {
        // Create a GET request directed to the /players route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log("Sending get request...");
            yield return request.SendWebRequest();

            Debug.Log($"GET all players.response code: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"GET success: {request.downloadHandler.text}");

                List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject <
                    List<Dictionary<string, string>>
                    > (request.downloadHandler.text);

                foreach(Dictionary<string, string> player in playerList)
                {
                    Debug.Log($"Got player: {player["nickname"]}");
                }
            }
            else
            {
                Debug.Log($"GET all error: {request.error}");
            }
        }
    }

    public void GetPlayer()
    {
        StartCoroutine(SampleGetPlayerRequest(1));
    }

    IEnumerator SampleGetPlayerRequest(int id)
    {
        // Create a GET request directed to the /players/:id route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players/" + id.ToString(), "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log("Sending get request...");
            yield return request.SendWebRequest();

            Debug.Log($"GET all players.response code: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"GET success: {request.downloadHandler.text}");

                Dictionary<string, string> player = JsonConvert.DeserializeObject<Dictionary<string, string>>
                    (request.downloadHandler.text);

                Debug.Log($"Got player: {player["nickname"]}");
            }
            else
            {
                Debug.Log($"GET all error: {request.error}");
            }
        }
    }

    public void UpdatePlayer()
    {
        StartCoroutine(SampleUpdatePlayerRequest(0));
    }

    IEnumerator SampleUpdatePlayerRequest(int id)
    {
        Dictionary<string, object> playerParams = new Dictionary<string, object>();

        playerParams.Add("id", id);
        playerParams.Add("newName", "John Smith");
        string requestString = JsonConvert.SerializeObject(playerParams);
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);
        Debug.Log(requestString);

        // Create a PUT request directed to the /players route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "PUT"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(requestData);

            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log("Sending put request...");
            yield return request.SendWebRequest();

            Debug.Log($"PUT update player.response code: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"PUT success: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"PUT error: {request.error}");
            }
        }
    }

    public void DeletePlayer()
    {
        StartCoroutine(SampleDeletePlayerRequest(0));
    }

    IEnumerator SampleDeletePlayerRequest(int id)
    {
        Dictionary<string, object> playerParams = new Dictionary<string, object>();

        playerParams.Add("id", id);
        string requestString = JsonConvert.SerializeObject(playerParams);
        byte[] requestData = Encoding.UTF8.GetBytes(requestString);
        Debug.Log(requestString);

        // Create a DELETE request directed to the /players route
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "DELETE"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(requestData);
            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log("Sending delete request...");
            yield return request.SendWebRequest();

            Debug.Log($"DELETE player.response code: {request.responseCode}");

            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log($"DELETE success: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"DELETE error: {request.error}");
            }
        }
    }
}
