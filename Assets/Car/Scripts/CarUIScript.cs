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

    private int checkpointCount = 0;
    private List<float> checkpointTimes = new List<float>();

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
        UIManager.DrawText(speedGuid, getSpeed().ToString() + " km/h", 24, new Color(0 / 255f, 145 / 255f, 234 / 255f), TextAnchor.LowerRight, new Vector2(0, Screen.height * 0.045f));
        UIManager.DrawText(checkpointTimesGuid, GetCheckpointTimes(), 24, new Color(0/255f, 145/255f, 234/255f), TextAnchor.UpperRight, new Vector2(0, -Screen.height* 0.04f));
        UIManager.DrawText(lapTimeGuid, GetLapTimes(), 30, new Color(76/255f, 175/255f, 80/255f), TextAnchor.UpperRight);
        UIManager.DrawText(positionGuid, "#" + GetComponent<CarRaceTimeScript>().GetPosition().ToString(), 32, Color.red, TextAnchor.LowerLeft);
        UIManager.DrawText(rpmGuid, getRPM().ToString() + " RPM", 18, new Color(0 / 255f, 145 / 255f, 234 / 255f), TextAnchor.LowerRight);
    }

    string GetLapTimes()
    {
        return GetComponent<CarRaceTimeScript>().GetLastLapTime() > 0 ? (GetComponent<CarRaceTimeScript>().GetLastLapTime()).ToString("0.000") : "";
    }
    string GetCheckpointTimes()
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

    int getSpeed()
    {
        return GetComponent<CarControllerScript>().getSpeed();
    }

    int getRPM()
    {
        return GetComponent<CarControllerScript>().GetEngineRPM();
    }
}
