using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioClip cardFlipSound;
    public AudioClip cardMatchSound;
    public AudioClip cardMismatchSound;
    public AudioClip gameOverSound;
    public AudioClip resetFlipSound;
    public int poolSize = 5;
    public AudioSource[] audioSources;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CreateAudioSourcePool();
        }
        else
        {
            Destroy(this);
        }
    }


    private void CreateAudioSourcePool()
    {
        audioSources = new AudioSource[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            GameObject audioSourceObject = new GameObject("AudioSource_" + i);
            audioSourceObject.transform.SetParent(transform);
            AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
            audioSources[i] = audioSource;
        }
    }
    // play sound
    public void PlaySound(AudioClip clip, bool loop = false)
    {
        foreach (var audioSource in audioSources)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
                return;
            }
        }
    }
}
