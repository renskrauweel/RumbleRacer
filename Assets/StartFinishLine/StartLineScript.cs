using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineScript : MonoBehaviour
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
        if (!carRaceTimeScript.GetHitStartLine()) carRaceTimeScript.SetRaceTimeStart();
    }
}
