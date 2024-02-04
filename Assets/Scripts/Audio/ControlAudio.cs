using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAudio : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip myClip;
    public float volume = 1f;
    public bool useLoop = false;

    public void StartAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myClip;
        audioSource.volume = volume;
        audioSource.loop = useLoop;

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
