using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    private AudioSource audioSource;
    public AudioClip audioOnClick;
    public AudioClip audioOnHover;
    public AudioClip audioOnGameOver;
    public AudioClip audioOnGameWin;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOnClick()
    {
        if (GameManager.Instance.isAudioOn)
        {
            audioSource.PlayOneShot(audioOnClick);
        }
    }

    public void PlayOnHover()
    {
        if (GameManager.Instance.isAudioOn)
        {
            audioSource.PlayOneShot(audioOnHover);
        }
    }

    public void PlayOnGameOver()
    {
        if (GameManager.Instance.isAudioOn)
        {
            audioSource.PlayOneShot(audioOnGameOver);
        }
    }
    public void PlayOnGameWin()
    {
        if (GameManager.Instance.isAudioOn)
        {
            audioSource.PlayOneShot(audioOnGameWin);
        }
    }
    
}
