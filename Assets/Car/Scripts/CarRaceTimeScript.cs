using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CarRaceTimeScript : MonoBehaviour
{
    private int totalCheckpointCount = 1;
    private int laps = 1;
    private List<DateTime> checkpointTimes = new List<DateTime>();
    private List<List<DateTime>> LapTimes = new List<List<DateTime>>();
    public int circuitNumber;

    void Start()
    {
        totalCheckpointCount = GameObject.FindGameObjectsWithTag("Checkpoint").ToList().Where(x => x.GetComponent<CheckpointScript>().circuitNumber == this.circuitNumber).ToList().Count;
        laps = GameObject.Find("GameManager").GetComponent<GameManager>().laps;
    }

    public void AddCheckpointHit()
    {
        checkpointTimes.Add(DateTime.Now);
    }
    public int GetCheckpointsHit()
    {
        return checkpointTimes.Count;
    }

    public bool HasHitAllCheckPoints()
    {
        return (totalCheckpointCount == checkpointTimes.Count);
    }

    public bool GetHitFinishLine()
    {
        return (totalCheckpointCount < checkpointTimes.Count);
    }

    public float GetLastCheckpointTime()
    {
        return (float)(checkpointTimes.Last() - checkpointTimes.First()).TotalMilliseconds/1000;
    }

    public int GetTotalCheckpointCount()
    {
        return totalCheckpointCount;
    }

    public void resetScript()
    {
        checkpointTimes = new List<DateTime>();
    }
}
