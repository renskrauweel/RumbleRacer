using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class CarRaceTimeScript : MonoBehaviour
{
    private int totalCheckpointCount;
    private int laps = 1;
    private List<DateTime> checkpointTimes = new List<DateTime>();
    private List<List<DateTime>> lapTimes = new List<List<DateTime>>();
    public int circuitNumber;

    void Start()
    {
        laps = GameObject.Find("GameManager").GetComponent<GameManager>().laps;
        totalCheckpointCount = GameObject.FindGameObjectsWithTag("Checkpoint").ToList().Where(x => x.GetComponent<CheckpointScript>().circuitNumber == this.circuitNumber).ToList().Count;
    }

    public void AddCheckpointHit()
    {
        checkpointTimes.Add(DateTime.Now);
    }
    public void AddCompleteLap()
    {
        lapTimes.Add(checkpointTimes);
        checkpointTimes = new List<DateTime>();
    }
    public int GetCheckpointsHit()
    {
        return checkpointTimes.Count;
    }

    public bool GetCompletedLap()
    {
        return (totalCheckpointCount == checkpointTimes.Count);
    }

    public bool GetCompletedRace()
    {
        return (laps == lapTimes.Count);
    }

    public float GetCurrentLapTime()
    {
        if (checkpointTimes.Count > 0)
            return (float)(checkpointTimes.Last() - checkpointTimes.First()).TotalMilliseconds / 1000;
        else
            return 0;
    }

    public float GetLastLapTime()
    {
        if (lapTimes.Count > 0)
            return (float)(lapTimes.Last().Last() - lapTimes.Last().First()).TotalMilliseconds / 1000;
        else
            return 0;
    }

    public float GetTotalRaceTime()
    {
        if (lapTimes.Count > 0)
            return (float)(lapTimes.Last().Last() - lapTimes.First().First()).TotalMilliseconds / 1000;
        else
            return (float)(checkpointTimes.Last() - checkpointTimes.First()).TotalMilliseconds / 1000;
    }

    public int GetTotalCheckpointCount()
    {
        return totalCheckpointCount;
    }

    public void resetScript()
    {
        checkpointTimes = new List<DateTime>();
        lapTimes = new List<List<DateTime>>();       
    }
}
