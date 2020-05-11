using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private CarRaceTimeScript carRaceTimeScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        carRaceTimeScript = other.gameObject.GetComponentInParent<CarRaceTimeScript>();
        carRaceTimeScript.AddCheckpointHit();
        Destroy(gameObject);

        List<DateTime> checkpointTimes = carRaceTimeScript.GetCheckPointTimes();
        DateTime prev = carRaceTimeScript.GetRaceTimeStart();
        
        Debug.Log("Checkpoint "+checkpointTimes.Count+": "+(int)(DateTime.Now-prev).TotalMilliseconds+"MS");
    }
}
