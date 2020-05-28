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
    private List<int> averageRPM = new List<int>();

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
        UIManager.DrawText(positionGuid, GetPosistion(), 32, Color.red, TextAnchor.LowerLeft);
        UIManager.DrawText(rpmGuid, getRPM().ToString() + " RPM", 18, new Color(0 / 255f, 145 / 255f, 234 / 255f), TextAnchor.LowerRight);
    }

    string GetLapTimes()
    {
        if (GetComponent<CarRaceTimeScript>().GetHitFinishLine())
        {
            return (GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs()/1000).ToString("0.000");
        }
        return "";
    }
    string GetCheckpointTimes()
    {
        string output = "";
        float checkpointTime = Math.Abs(Convert.ToSingle((DateTime.Now - GetComponent<CarRaceTimeScript>().GetRaceTimeStart()).TotalMilliseconds) / 1000);
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

    float getSpeed()
    {
        return Math.Abs(Convert.ToSingle(Math.Round(transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z)));
    }

    int getRPM()
    {
        int rpm = Convert.ToInt32(GetComponent<CarControllerScript>().WheelRR.rpm+
        GetComponent<CarControllerScript>().WheelRR.rpm+
        GetComponent<CarControllerScript>().WheelRR.rpm+
        GetComponent<CarControllerScript>().WheelRR.rpm) / 4;
        averageRPM.Add(rpm);
        if (averageRPM.Count > 150)
        {
            averageRPM.RemoveAt(0);
        }
        return Convert.ToInt32(Math.Abs(averageRPM.Average()));
    }

    string GetPosistion()
    {
        return "#1";
    }
}
