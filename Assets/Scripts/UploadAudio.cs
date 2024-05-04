using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class UploadAudio : MonoBehaviour
{
    // URL of your API
    [SerializeField] private string apiUrl = "https://smart-shopper-api.azurewebsites.net/api/askAgent?";

    private string path = "C:/Users/FA507/Documents/GitHub/smARtShopper/Assets/wavFile.wav";

    void Start()
    {
        APISendRequest(path);
    }

    public void APISendRequest(string audioFilePath){
    
        StartCoroutine(UploadAudioFile(audioFilePath));

    }

    IEnumerator UploadAudioFile(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError("File not found: " + filePath);
            yield break;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        // Add the binary file data to the form
        form.AddBinaryData("audiofile", fileData, Path.GetFileName(filePath), "audio/wav");

        using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, form))
        {
            request.chunkedTransfer = false;  // Disable chunked transfer
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}