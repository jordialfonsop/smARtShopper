using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip recordedClip;

    [SerializeField] UploadAudio uploadAudio;
    [SerializeField] RecordButton recordButton;

    

    private bool isActive = false;
    public void ButtonPress()
    {
        if (isActive)
        {
            StopRecording();
            isActive = false;
        }
        else
        {
            StartRecording();
            isActive = true;
        }
    }
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void StartRecording()
    {
        // Start recording with a 1 second length limit
        recordedClip = Microphone.Start(null, false, 5, 44100);
        //recordButton.SetButtonColor(new Color(255, 0, 0, 60));
    }

    public void StopRecording()
    {
        Microphone.End(null);
        // Play the recorded audio
        audioSource.clip = recordedClip;
        SavWav.SaveWav("request", recordedClip);
        uploadAudio.APISendRequest(uploadAudio.path);
        //audioSource.Play();
        //recordButton.SetButtonColor(new Color(255,255,255,60));
    }
}