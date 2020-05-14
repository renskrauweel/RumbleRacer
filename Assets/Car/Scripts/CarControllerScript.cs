﻿using UnityEngine;
using System.Collections;
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
    float maxFwdSpeed = -3000;
    float maxBwdSpeed = 1000f;
    float gravity = 9.8f;
    private bool controllable = true;
    private bool braked = false;
    private float maxBrakeTorque = 1000;
    private Rigidbody rb;
    public Transform centreofmass;
    private float maxTorque = 10000;
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        rb.centerOfMass = centreofmass.transform.localPosition;
    }

    void FixedUpdate()
    {
        if (!braked)
        {
            WheelFL.brakeTorque = 0;
            WheelFR.brakeTorque = 0;
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }

        if (controllable)
        {
            //speed of car, Car will move as you will provide the input to it.

            WheelRR.motorTorque = maxTorque * Input.GetAxis("Vertical");
            WheelRL.motorTorque = maxTorque * Input.GetAxis("Vertical");

            //changing car direction
            //Here we are changing the steer angle of the front tyres of the car so that we can change the car direction.
            WheelFL.steerAngle = 50 * (Input.GetAxis("Horizontal"));
            WheelFR.steerAngle = 50 * Input.GetAxis("Horizontal");
        }
    }

    public void AIController(float vertical, float horizontal, float jump)
    {
        if (!braked)
        {
            WheelFL.brakeTorque = 0;
            WheelFR.brakeTorque = 0;
            WheelRL.brakeTorque = 0;
            WheelRR.brakeTorque = 0;
        }

        if (controllable)
        {
            //speed of car, Car will move as you will provide the input to it.

            WheelRR.motorTorque = maxTorque * vertical;
            WheelRL.motorTorque = maxTorque * vertical;

            //changing car direction
            //Here we are changing the steer angle of the front tyres of the car so that we can change the car direction.
            WheelFL.steerAngle = 50 * horizontal;
            WheelFR.steerAngle = 50 * horizontal;
        }

        if (jump > 0.5)
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
    }

    void Update()
    {
        HandBrake();

        //for tyre rotate
        WheelFLtrans.Rotate(WheelFL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelFRtrans.Rotate(WheelFR.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRLtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        WheelRRtrans.Rotate(WheelRL.rpm / 60 * 360 * Time.deltaTime, 0, 0);
        //changing tyre direction
        Vector3 temp = WheelFLtrans.localEulerAngles;
        Vector3 temp1 = WheelFRtrans.localEulerAngles;
        temp.y = WheelFL.steerAngle - (WheelFLtrans.localEulerAngles.z);
        //temp.x = 90;
        WheelFLtrans.localEulerAngles = temp;
        temp1.y = WheelFR.steerAngle - WheelFRtrans.localEulerAngles.z + 180;
        WheelFRtrans.localEulerAngles = temp1;
        eulertest = WheelFLtrans.localEulerAngles;
    }
    void HandBrake()
    {
        //Debug.Log("brakes " + braked);
        if (Input.GetButton("Jump"))
        {
            braked = true;
        }
        else
        {
            braked = false;
        }
        if (braked)
        {

            WheelRL.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRR.brakeTorque = maxBrakeTorque * 20;//0000;
            WheelRL.motorTorque = 0;
            WheelRR.motorTorque = 0;
        }
    }

    public void SetControllable(bool controllable)
    {
        this.controllable = controllable;
    }

    public bool IsControllable()
    {
        return controllable;
    }
}