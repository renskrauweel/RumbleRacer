using System;
using Lib.Services;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public bool StartAiStream = false;
    private AudioSource _audioSource;
    public AudioClip BackgroundMusic1;
    public AudioClip BackgroundMusic2;
    public AudioClip BackgroundMusic3;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StartAiStream) DoStartAiStream();
        _audioSource = GetComponent<AudioSource>();
        
        setRandomBackgroundMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoStartAiStream()
    {
        try
        {
            StreamService streamService = new StreamService();
            streamService.StartStream("Rens is cool");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void setRandomBackgroundMusic()
    {
        Random r = new Random();
        switch (r.Next(3))
        {
            case 0: 
                _audioSource.PlayOneShot(BackgroundMusic1, .25f);
                break;
            case 1: 
                _audioSource.PlayOneShot(BackgroundMusic2, .25f);
                break;
            case 2: 
                _audioSource.PlayOneShot(BackgroundMusic3, .25f);
                break;
        }
    }
}
