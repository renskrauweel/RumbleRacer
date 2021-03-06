﻿using UnityEngine;
using System;
using System.Linq;

public class CarControllerScript : MonoBehaviour
{
    public WheelCollider WheelFL;
    public WheelCollider WheelFR;
    public WheelCollider WheelRL;
    public WheelCollider WheelRR;
    public Transform WheelFLtrans;
    public Transform WheelFRtrans;
    public Transform WheelRLtrans;
    public Transform WheelRRtrans;
    public Vector3 eulertest;
    private bool controllable;
    private float maxBrakeTorque = 1000;
    private Rigidbody rb;
    public Transform centreofmass;
    private float maxTorque = 10000;
    private float vertical;
    private float horizontal;
    private float jump;

    public int shiftRPM = 6000;
    public int idleRPM = 800;
    private float[] gearRatios = { 3.3f, 3.321f, 2.077f, 1.308f, 1f, 0.864f };
    private float diffRatio = 3.9f;
    private float circumferenceMeters = 1.9277f;
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centreofmass.transform.localPosition;
        controllable = !GameObject.Find("GameManager").GetComponent<GameManager>().countdown;
    }

    void FixedUpdate()
    {
        if (gameObject.CompareTag("Car"))
        {
            jump = Convert.ToSingle(Input.GetButton("Jump"));           
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            control();
        }
    }

    public void AIController(float vertical, float horizontal, float jump)
    {
        this.jump = jump;
        this.vertical = vertical;
        this.horizontal = horizontal;
        control();
    }

    private void control()
    {
        if (controllable)
        {
            if (jump < 0.5)
            {
                WheelFL.brakeTorque = 0;
                WheelFR.brakeTorque = 0;
                WheelRL.brakeTorque = 0;
                WheelRR.brakeTorque = 0;
            }
            else
            {
                WheelRL.brakeTorque = maxBrakeTorque * 20;
                WheelRR.brakeTorque = maxBrakeTorque * 20;
                WheelRL.motorTorque = 0;
                WheelRR.motorTorque = 0;
            }

            //speed of car, Car will move as you will provide the input to it.

            WheelRR.motorTorque = maxTorque * vertical;
            WheelRL.motorTorque = maxTorque * vertical;

            //changing car direction
            //Here we are changing the steer angle of the front tyres of the car so that we can change the car direction.
            WheelFL.steerAngle = 50 * horizontal;
            WheelFR.steerAngle = 50 * horizontal;
        }
    }

    void Update()
    {
        //for tyre rotate
        WheelFLtrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelFRtrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRLtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRRtrans.Rotate(WheelRR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        //changing tyre direction
        Vector3 temp = WheelFLtrans.localEulerAngles;
        Vector3 temp1 = WheelFRtrans.localEulerAngles;
        temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.z);
        WheelFLtrans.localEulerAngles = temp;
        temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.z + 180;
        WheelFRtrans.localEulerAngles = temp1;
        eulertest = WheelFLtrans.localEulerAngles;
    }

    public void SetControllable(bool controllable)
    {
        this.controllable = controllable;
    }

    public int GetEngineRPM()
    {
        float WheelRPM = transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z / 60 * 1000 / circumferenceMeters;
        int gear = 0;

        if (WheelRPM < 0)
            gear = 0;
        else
        {
            for (int i = 1; i < gearRatios.Count(); i++)
            {
                if (WheelRPM * gearRatios[i] * diffRatio < shiftRPM || i == gearRatios.Count() - 1)
                {
                    gear = i;
                    break;
                }
            }
        }

        int engineRPM = Math.Abs(Convert.ToInt32(WheelRPM * gearRatios[gear] * diffRatio));
        return engineRPM > idleRPM ? engineRPM : UnityEngine.Random.Range(idleRPM, idleRPM+30);
    }

    public int getSpeed()
    {
        return Math.Abs(Convert.ToInt32(transform.InverseTransformDirection(GetComponent<Rigidbody>().velocity).z));
    }
}