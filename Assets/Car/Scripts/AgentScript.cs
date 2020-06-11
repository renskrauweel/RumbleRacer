using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Sensors;
using System;
using System.Linq;
using System.Numerics;
using Unity.Mathematics;
using System.Text.RegularExpressions;
using Vector3 = UnityEngine.Vector3;

public class AgentScript : Unity.MLAgents.Agent
{
    public int circuitNumber;
    public Transform Target;
    private Rigidbody rBody;
    private Collision collision = null;
    public GameObject checkpointPrefab;
    private int lastCheckpointsHit = 0;
    private List<GameObject> checkpoints;
    private float fastestTime = float.MaxValue;
    private Vector3 startPos;
    private quaternion startRot;
    private bool isStarted = false;
    private float closestDistanceToNextCheckpoint = float.MaxValue;

    void Start()
    {
        if (!isStarted)
        {
            rBody = GetComponent<Rigidbody>();
            checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList().Where(x => x.GetComponent<CheckpointScript>().circuitNumber == this.circuitNumber).ToList();
            startPos = transform.position;
            startRot = transform.rotation;
        }
        isStarted = true;
    }

    public override void OnEpisodeBegin()
    {
        if (!isStarted) Start();
        ResetAI();        
    }

    private void ResetAI()
    {
        gameObject.GetComponent<CarRaceTimeScript>().resetScript();
        transform.position = startPos;
        transform.rotation = startRot;
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
        lastCheckpointsHit = 0;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Objects and their distance
        foreach(var Distance in GetComponent<RayCasterScript>().getListOfHitsDistance())
        {
            sensor.AddObservation(Distance / 500);
        }

        // Agent velocity
        sensor.AddObservation(Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).z / 160) / 2 + 0.5));
        sensor.AddObservation(Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).x / 160) / 2 + 0.5));
        sensor.AddObservation(Convert.ToSingle(transform.rotation.y / 360));
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        Debug.LogWarning(GetCumulativeReward());
        AddReward(-0.002f);

        GetComponent<CarControllerScript>().AIController(vectorAction[0], vectorAction[1], vectorAction[2]);

        float speed = Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).z / 160) / 2 + 0.5);
        GameObject activeCheckpoint = checkpoints.Where(x => x.GetComponent<CheckpointScript>().order == GetComponent<CarRaceTimeScript>().GetCheckpointsHit()).FirstOrDefault();
        if(activeCheckpoint != null)
        {
            float distanceToNextCheckpoint = Vector3.Distance(activeCheckpoint.GetComponent<Renderer>().bounds.ClosestPoint(transform.position), transform.position);
            if (speed > 0.525 && distanceToNextCheckpoint < closestDistanceToNextCheckpoint)
            {
                AddReward(speed / 500);
                closestDistanceToNextCheckpoint = distanceToNextCheckpoint;
        
                if (closestDistanceToNextCheckpoint < 6f)
                {
                    closestDistanceToNextCheckpoint = float.MaxValue;
                }
            }
        }

        if (lastCheckpointsHit < GetComponent<CarRaceTimeScript>().GetCheckpointsHit())
        {
            AddReward(0.25f);
            lastCheckpointsHit = GetComponent<CarRaceTimeScript>().GetCheckpointsHit();
        }

        if(collision != null && collision.gameObject.tag == "Barrier")
        {
            AddReward(-0.01f);
        }     
        if(collision != null && collision.gameObject.tag == "CarAI")
        {
            AddReward(-0.005f);
        }

        if (GetComponent<CarRaceTimeScript>().GetCompletedRace())
        {
            if(GetComponent<CarRaceTimeScript>().GetTotalRaceTime() < fastestTime)
            {
                fastestTime = GetComponent<CarRaceTimeScript>().GetTotalRaceTime();
                //Debug.LogError("NEW FASTEST TIME: " + fastestTime);
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