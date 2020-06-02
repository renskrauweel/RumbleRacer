using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib.Replay;
using Lib.Services;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{
    public int laps = 1;
    public int OpponentCount = 4;
    public GameObject AiCarPrefab;
    public GameObject PlayerCarPrefab;
    public bool countdown = true;
    public bool StartAiStream = false;
    public bool MuteBackgroundMusic = false;
    public string ReplayLogPath = String.Empty;
    public GameObject Ghost;
    private AudioSource _audioSource;
    public AudioClip BackgroundMusic1;
    public AudioClip BackgroundMusic2;
    public AudioClip BackgroundMusic3;
    private LoggingService _loggingService = new LoggingService();
    private List<ReplayState> _replayStates = new List<ReplayState>();
    private List<float> _replayStateTimes = new List<float>();
    private ReplayService _replayService = new ReplayService();

    // Start is called before the first frame update
    void Start()
    {
        // Register exception log callback
        Application.logMessageReceived += ApplicationOnlogMessageReceived;
        DontDestroyOnLoad(gameObject);

        if (ReplayLogPath.Length > 0) InitReplay();
        
        if (StartAiStream) DoStartAiStream();
        _audioSource = GetComponent<AudioSource>();

        if (!MuteBackgroundMusic) SetRandomBackgroundMusic();
        if (countdown) StartCountdown();
        
        SetOpponentCount();
        SpawnCars();
    }

    private void ApplicationOnlogMessageReceived(string condition, string stacktrace, LogType type)
    {
        if (type == LogType.Exception) _loggingService.LogException(condition, stacktrace);
    }

    // Update is called once per frame
    void Update()
    {
        if (_replayStates.Count > 0) _replayService.UpdateReplayState(Ghost, _replayStates, _replayStateTimes);
    }

    private void StartCountdown()
    {
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach(car => car.GetComponent<CountdownScript>().StartCountdown());
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

    private void SetRandomBackgroundMusic()
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

    private void InitReplay()
    {
        try
        {
            // Instantiate ghost
            Ghost = Instantiate(Ghost, new Vector3(0, 0.5f, 0), Quaternion.identity);
            
            using (StreamReader sr = new StreamReader(ReplayLogPath))
                _replayService.InitReplay(sr.ReadToEnd(), _replayStates, _replayStateTimes);
        }
        catch (IOException e)
        {
            Debug.Log("The file could not be read. Replay is not available");
        }
    }

    private void SpawnCars()
    {
        List<GameObject> spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();

        bool playerAtSpawn = false;
        int spawnsOccupied = 0;
        GameObject Car;
        foreach (GameObject spawnPoint in spawnPoints)
        {
            if (!playerAtSpawn && PlayerCarPrefab != null)
            {
                Instantiate(PlayerCarPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                playerAtSpawn = true;
                spawnsOccupied++;
            } else if (AiCarPrefab != null && spawnsOccupied <= OpponentCount)
            {
                Car = Instantiate(AiCarPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                Car.transform.position = spawnPoint.transform.position;
                spawnsOccupied++;
            }
        }
    }

    private void SetOpponentCount()
    {
        OpponentCount = int.TryParse(PlayerPrefs.GetString("opponentcount"), out var number) ? number : 0;
    }
}
