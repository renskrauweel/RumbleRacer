using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineScript : MonoBehaviour
{
    private CarRaceTimeScript carRaceTimeScript;

    private void OnTriggerEnter(Collider other)
    {
        carRaceTimeScript = other.gameObject.GetComponentInParent<CarRaceTimeScript>();
        if (!carRaceTimeScript.GetHitStartLine()) carRaceTimeScript.SetRaceTimeStart();
    }
}
