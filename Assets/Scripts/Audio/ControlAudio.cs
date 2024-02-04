using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip myClip;
    public float volume = 1f;

    public void StartAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }   
}
