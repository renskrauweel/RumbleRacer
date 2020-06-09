using System;
using Lib.Services;
using UnityEngine;

public class CarReplayLoggerScript : MonoBehaviour
{
    private CarControllerScript _carControllerScript;
    private LoggingService _loggingService;
    private Guid _carReplayGuid;
    public bool UseLogging = true;
    
    void Start()
    {
        _carControllerScript = GetComponent<CarControllerScript>();
        _loggingService = new LoggingService();
        _carReplayGuid = _loggingService.GetCarReplayGuid();
        
        UseLogging = (PlayerPrefs.GetInt("createghost") == 1);
    }
    
    void Update()
    {
        if (UseLogging)
        {
            var transform = _carControllerScript.transform;
            _loggingService.LogCarReplay(_carReplayGuid, Time.timeSinceLevelLoad, transform.position, transform.rotation);
        }
    }
}
