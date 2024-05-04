using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRecorder : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip recordedClip;

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
        recordedClip = Microphone.Start(null, false, 100, 44100);
    }

    public void StopRecording()
    {
        Microphone.End(null);
        // Play the recorded audio
        audioSource.clip = recordedClip;
        SavWav.SaveWav("request", recordedClip);
        //audioSource.Play();
    }
}