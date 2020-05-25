using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Sensors;
using System;
using System.Linq;
using Unity.Mathematics;

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

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint").ToList().Where(x => x.GetComponent<CheckpointScript>().circuitNumber == this.circuitNumber).ToList();
        startPos = transform.position;
        startRot = transform.rotation;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startPos;
        transform.rotation = startRot;
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
            sensor.AddObservation(ray / 300);
        }
        
        // Agent velocity
        sensor.AddObservation(Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).z / 100) / 2 + 0.5));
        sensor.AddObservation(Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).x / 100) / 2 + 0.5));
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        AddReward(-0.002f);

        if (Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).z / 100) / 2 + 0.5) > 0.525)
        {
            AddReward(Convert.ToSingle((transform.InverseTransformDirection(rBody.velocity).z / 100) / 2 + 0.5) / 100);
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
                fastestTime = GetComponent<CarRaceTimeScript>().GetCurrentRaceTimeMs();
                Debug.LogError("NEW FASTEST TIME: " + fastestTime);
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