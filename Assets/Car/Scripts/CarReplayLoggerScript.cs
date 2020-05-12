using System;
using Lib.Services;
using UnityEngine;

public class CarReplayLoggerScript : MonoBehaviour
{
    private CarControllerScript _carControllerScript;
    private LoggingService _loggingService;
    private Guid _carReplayGuid;
    public bool UseLogging = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _carControllerScript = GetComponent<CarControllerScript>();
        _loggingService = new LoggingService();
        _carReplayGuid = _loggingService.GetCarReplayGuid();
    }

    // Update is called once per frame
    void Update()
    {
        if (UseLogging)
        {
            var transform = _carControllerScript.transform;
            _loggingService.LogCarReplay(_carReplayGuid, Time.time * 1000, transform.position, transform.rotation);
        }
    }
}
