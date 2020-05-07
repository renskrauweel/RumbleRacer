using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
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

        if (!carRaceTimeScript.GetHitFinishLine() && carRaceTimeScript.HasHitAllCheckPoints())
        {
            carRaceTimeScript.SetCurrentRaceTime();
            carRaceTimeScript.SetHitFinishLine(true);
            Debug.Log("Finished in "+carRaceTimeScript.GetCurrentRaceTimeMs()+"MS");
        } else if (!carRaceTimeScript.HasHitAllCheckPoints())
        {
            Debug.Log("You haven't hit all checkpoints!");
        }
    }
}
