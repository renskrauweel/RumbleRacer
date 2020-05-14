using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class CarRaceTimeScript : MonoBehaviour
{
    private DateTime raceTimeStart = new DateTime();
    private DateTime raceTimeCurrent = new DateTime();
    private bool hitStartLine = false;
    private bool hitFinishLine = false;
    private int checkpointsHit = 0;
    private int totalCheckPointCount = 0;
    private List<DateTime> checkpointTimes = new List<DateTime>();
    public bool useTimer = true;
    private System.Timers.Timer timer = new System.Timers.Timer();
    
    // Start is called before the first frame update
    void Start()
    {
        totalCheckPointCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetRaceTimeStart()
    {
        raceTimeStart = DateTime.Now;
        hitStartLine = true;
        
        // Run timer
        if (useTimer)
        {
            timer.Elapsed += RunTimer;
            timer.Interval = 1000;
            timer.Enabled = true;
        }
    }

    public void resetScript()
    {
        raceTimeStart = new DateTime();
        raceTimeCurrent = new DateTime();
        hitStartLine = false;
        hitFinishLine = false;
        checkpointsHit = 0;
        totalCheckPointCount = GameObject.FindGameObjectsWithTag("Checkpoint").Length;
        checkpointTimes = new List<DateTime>();
        useTimer = true;
        timer = new System.Timers.Timer();
    }

    private void RunTimer(object source, ElapsedEventArgs e)
    {
        Debug.Log((DateTime.Now-raceTimeStart).TotalMilliseconds+"MS");
    }

    public DateTime GetRaceTimeStart()
    {
        return raceTimeStart;
    }
    
    public void SetCurrentRaceTime()
    {
        raceTimeCurrent = DateTime.Now;
    }

    public float GetCurrentRaceTimeMs()
    {
        return (int)(raceTimeCurrent - raceTimeStart).TotalMilliseconds;
    }

    public bool GetHitStartLine()
    {
        return hitStartLine;
    }
    
    public bool GetHitFinishLine()
    {
        return hitFinishLine;
    }

    public void SetHitFinishLine(bool hitFinishLine)
    {
        this.hitFinishLine = hitFinishLine;
        timer.Enabled = false;
    }

    public void AddCheckpointHit()
    {
        checkpointsHit++;
        checkpointTimes.Add(DateTime.Now);
    }

    public bool HasHitAllCheckPoints()
    {
        return (totalCheckPointCount == checkpointsHit);
    }

    public List<DateTime> GetCheckPointTimes()
    {
        return checkpointTimes;
    }

    public int GetCheckpointsHit()
    {
        return checkpointsHit;
    }
}
