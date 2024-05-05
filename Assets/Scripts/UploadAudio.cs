using System.Collections;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class UploadAudio : MonoBehaviour
{
    // URL of your API
    [SerializeField] private string apiUrl = "https://smart-shopper-api.azurewebsites.net/api/askAgent?";

    public string path;

    [SerializeField] TMP_Text uitext;
    [SerializeField] public TaskManager taskManager;

    void Start()
    {
        path = Application.persistentDataPath + "/request.wav";
        //path = "C:/Users/FA507/Documents/GitHub/smARtShopper/Assets/wavFile.wav";
        //APISendRequest(path);
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
                uitext.text = "Error: " + request.error;
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                //uitext.text = "Response: " + request.downloadHandler.text;
                //Debug.Log(uitext.text);
                taskManager.GetTaskInformation(request.downloadHandler.text);
            }
        }
    }
}