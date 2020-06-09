using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarUIScript : MonoBehaviour
{
    private UIManagerScript UIManager;
    private Guid rpmGuid;
    private Guid lapTimeGuid;
    private Guid positionGuid;
    private Guid checkpointTimesGuid;
    private Guid speedGuid;

    private List<float> checkpointTimes = new List<float>();
    private int checkpointCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        UIManager = GetComponentInChildren<UIManagerScript>();
        rpmGuid = Guid.NewGuid();
        lapTimeGuid = Guid.NewGuid();
        positionGuid = Guid.NewGuid();
        checkpointTimesGuid = Guid.NewGuid();
        speedGuid = Guid.NewGuid();
    }

    void Update()
    {
        UIManager.DrawText(speedGuid, getSpeed().ToString() + " km/h", 24, Color.white, TextAnchor.LowerRight, new Vector2(0, Screen.height * 0.045f));
        UIManager.DrawText(checkpointTimesGuid, getCurrentCheckpointTime(), 24, Color.white, TextAnchor.UpperRight, new Vector2(0, -Screen.height* 0.035f));
        UIManager.DrawText(lapTimeGuid, GetLapTimes(), 30, Color.white, TextAnchor.UpperRight, new Vector2(0, 0));
        UIManager.DrawText(positionGuid, "#" + GetComponent<CarRaceTimeScript>().GetPosition().ToString(), 32, Color.yellow, TextAnchor.UpperLeft, new Vector2(Screen.width * 0.002f, -Screen.height * 0.02f));
        UIManager.DrawText(rpmGuid, getRPM().ToString() + " RPM", 18, Color.white, TextAnchor.LowerRight, new Vector2(0, Screen.height * 0.02f));
    }

    string getCurrentCheckpointTime()
    {
        string output = "";
        float checkpointTime = Math.Abs(GetComponent<CarRaceTimeScript>().GetCurrentLapTime());
        if (GetComponent<CarRaceTimeScript>().GetCheckpointsHit() > checkpointCount)
        {
            checkpointCount = GetComponent<CarRaceTimeScript>().GetCheckpointsHit();
            checkpointTimes.Add(checkpointTime);
            if (checkpointTimes.Count > 5)
            {
                checkpointTimes.RemoveAt(0);
            }
        }
        checkpointTimes.Reverse();
        checkpointTimes.ForEach(time => output += time.ToString("0.000") + Environment.NewLine);
        checkpointTimes.Reverse();
        return output;

    }

    string GetLapTimes()
    {
        return GetComponent<CarRaceTimeScript>().GetLastLapTime() > 0 ? (GetComponent<CarRaceTimeScript>().GetLastLapTime()).ToString("0.000") : "";
    }

    int getSpeed()
    {
        return GetComponent<CarControllerScript>().getSpeed();
    }

    int getRPM()
    {
        return GetComponent<CarControllerScript>().GetEngineRPM();
    }
}
