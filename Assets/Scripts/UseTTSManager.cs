using UnityEngine;
using UnityEngine.Networking;

using System.Collections;


public class UseTTSManager : MonoBehaviour
{
    public TTSManager ttsManager;
    public string apiUrl = "https://smart-shopper-api.azurewebsites.net/api/getLocation";
    public string text = "pot";
    void Start()
    {
        // You can assign ttsManager via the Unity Editor or find it dynamically if it's already in the scene.
        if (ttsManager == null)
        {
            ttsManager = FindObjectOfType<TTSManager>();
        }
        //StartCoroutine(CallAPI("OTINET 125ml"));
        //Debug.Log("patata");

    }

    // This method could be called by a UI button click or other event.
    public void StartSynthesis(string textToSynthesize)
    {
        if (ttsManager != null)
        {
            //string textToSynthesize = "Hello, this is a test of the TTS system.";
            // Optionally, you can specify the model, voice, and speed.
            TTSModel model = TTSModel.TTS_1;   // Example model
            TTSVoice voice = TTSVoice.Alloy;   // Example voice
            float speed = 1.0f;                // Normal speed

            // Calling SynthesizeAndPlay with parameters
            ttsManager.SynthesizeAndPlay(textToSynthesize, model, voice, speed);
        }
        else
        {
            Debug.LogError("TTSManager component is not assigned or found in the scene.");
        }
    }

    IEnumerator CallAPI(string objectName)
    {
        // Constructing the URL with query parameter
        string urlWithParams = apiUrl + "?objectName=" + objectName;//WWW.EscapeURL(objectName);
        Debug.Log("pat: " + urlWithParams);
        // Using UnityWebRequest to handle the API call
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlWithParams))
        {
            // Send the request and wait for the response
            yield return webRequest.SendWebRequest();

            // Check for errors
            if (webRequest.isNetworkError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else if (webRequest.isHttpError)
            {
                Debug.LogError("HTTP Error: " + webRequest.error);
            }
            else
            {
                // Process the response
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                text = webRequest.downloadHandler.text;
                StartSynthesis(text);

            }
        }
    }
}

