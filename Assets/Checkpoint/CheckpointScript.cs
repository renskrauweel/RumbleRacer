using System;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{

    public int order;
    private CarRaceTimeScript carRaceTimeScript;
    private void OnTriggerEnter(Collider other)
    {
        carRaceTimeScript = other.gameObject.GetComponentInParent<CarRaceTimeScript>();
        if(carRaceTimeScript.GetCheckpointsHit() == order - 1)
        {
            carRaceTimeScript.AddCheckpointHit();
            gameObject.SetActive(false);

            List<DateTime> checkpointTimes = carRaceTimeScript.GetCheckPointTimes();
            DateTime prev = carRaceTimeScript.GetRaceTimeStart();

            Debug.Log("Checkpoint " + checkpointTimes.Count + ": " + (int)(DateTime.Now - prev).TotalMilliseconds + "MS");
        }        
    }
}
