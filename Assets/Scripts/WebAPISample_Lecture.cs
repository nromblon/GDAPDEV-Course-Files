using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;
using UnityEngine.Networking;

public class WebAPISample_Lecture : MonoBehaviour
{
    public readonly string BaseURL = "https://gdapdev-web-api.herokuapp.com/api/";

    public void CreatePlayer()
    {
        StartCoroutine(SamplePostRequest());
    }

    IEnumerator SamplePostRequest()
    {
        Dictionary<string, string> playerParams = new Dictionary<string, string>();

        playerParams.Add("name", "Bob Bobson");
        playerParams.Add("nickname", "Bob");
        playerParams.Add("email", "bob@bobmail.com");
        playerParams.Add("contact", "12345678910");

        string requestBody = JsonConvert.SerializeObject(playerParams);
        byte[] requestData = Encoding.UTF8.GetBytes(requestBody);
        Debug.Log(requestBody);

        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = new UploadHandlerRaw(requestData);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log("response code: " + request.responseCode);
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log("Success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("error: " + request.error);
            }
        }
    }

    public void GetPlayers()
    {
        StartCoroutine(SampleGetAllRequest());
    }

    IEnumerator SampleGetAllRequest()
    {
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players", "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log("response code: " + request.responseCode);
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log("Success: " + request.downloadHandler.text);
                List<Dictionary<string, string>> playerList = JsonConvert.DeserializeObject
                    <List<Dictionary<string, string>>>(request.downloadHandler.text);

                foreach(Dictionary<string, string> player in playerList)
                {
                    Debug.Log($"Got player {player["nickname"]}");
                }
            }
            else
            {
                Debug.Log("error: " + request.error);
            }
        }
    }

    public void GetPlayer()
    {
        StartCoroutine(SampleGetRequest(1));
    }

    IEnumerator SampleGetRequest(int id)
    {
        using (UnityWebRequest request = new UnityWebRequest(BaseURL + "players/" + id.ToString(), "GET"))
        {
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            Debug.Log("response code: " + request.responseCode);
            if (string.IsNullOrEmpty(request.error))
            {
                Debug.Log("Success: " + request.downloadHandler.text);
                Dictionary<string, string> player = JsonConvert.DeserializeObject
                    <Dictionary<string, string>>(request.downloadHandler.text);

                Debug.Log($"Got player {player["nickname"]}");
            }
            else
            {
                Debug.Log("error: " + request.error);
            }
        }
    }

}
