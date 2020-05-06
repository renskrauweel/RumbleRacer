using System;
using System.Linq;
using System.Threading.Tasks;
using Lib.Services;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public bool StartAiStream = false;
    public bool MuteBackgroundMusic = false;
    private AudioSource _audioSource;
    public AudioClip BackgroundMusic1;
    public AudioClip BackgroundMusic2;
    public AudioClip BackgroundMusic3;
    private bool countdown = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StartAiStream) DoStartAiStream();
        _audioSource = GetComponent<AudioSource>();

        if (!MuteBackgroundMusic) setRandomBackgroundMusic();
        if (countdown) StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async Task StartCountdown()
    {
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach(car => car.GetComponentInChildren<CountdownScript>().StartCountdown());
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
                _audioSource.PlayOneShot(BackgroundMusic1, .1f);
                break;
            case 1: 
                _audioSource.PlayOneShot(BackgroundMusic2, .1f);
                break;
            case 2: 
                _audioSource.PlayOneShot(BackgroundMusic3, .1f);
                break;
        }
    }
}
