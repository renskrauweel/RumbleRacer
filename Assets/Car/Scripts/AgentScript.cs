using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Sensors;
using System;
using System.Linq;

public class AgentScript : Unity.MLAgents.Agent
{

    public Transform Target;
    private Rigidbody rBody;
    private Collision collision = null;
    public GameObject checkpointPrefab;
    private int lastCheckpointsHit = 0;
    private List<GameObject> checkpoints;
    private float fastestTime = float.MaxValue;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = new Vector3(80,0,-95);
        transform.rotation = Quaternion.Euler(0, 60, 0);
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;

        checkpoints.ForEach(x => x.SetActive(true));
        GetComponent<CarRaceTimeScript>().resetScript();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        foreach(var ray in GetComponent<RayCasterScript>().getListOfHitsDistance())
        {
            sensor.AddObservation(ray / 100);
        }
        
        // Agent velocity
        sensor.AddObservation(transform.InverseTransformDirection(rBody.velocity).z / 100);
        sensor.AddObservation(transform.rotation.eulerAngles / 360f);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-0.005f);

        if (transform.InverseTransformDirection(rBody.velocity).z / 100 > 0)
        {
            AddReward(transform.InverseTransformDirection(rBody.velocity).z / 100);
        }

        GetComponent<CarControllerScript>().AIController(vectorAction[0], vectorAction[1], vectorAction[2]);

        if (lastCheckpointsHit < GetComponent<CarRaceTimeScript>().GetCheckpointsHit())
        {
            AddReward(0.5f);
            lastCheckpointsHit = GetComponent<CarRaceTimeScript>().GetCheckpointsHit();
        }

        if(collision != null && collision.gameObject.tag == "Barrier")
        {
            AddReward(-0.01f);
        }

        if (GetComponent<CarRaceTimeScript>().GetHitFinishLine())
        {
            if(GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs() < fastestTime)
            {
                if(fastestTime != float.MaxValue)
                {
                    //Debug.LogError(GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs() - fastestTime);
                    //AddReward(GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs() - fastestTime / 1000);
                }
                fastestTime = GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs();
            }
            AddReward(1f);
            EndEpisode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.collision = collision;
    }

    private void OnCollisionExit(Collision collision)
    {
        this.collision = null;
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = Input.GetAxis("Vertical");
        actionsOut[1] = Input.GetAxis("Horizontal");
        actionsOut[2] = Convert.ToSingle(Input.GetButton("Jump"));
    }
}