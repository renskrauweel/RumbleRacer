using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayCasterScript : MonoBehaviour
{
    public int timeoutDistance = 100;
    public bool ShowRays = false;
    public bool LogDistance = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ShowRays)
            drawRays();
        if (LogDistance)
            Debug.Log(getDebugMessageDistance());   
    }  

    private List<Ray> getListOfRays()
    {
        List<Ray> listOfRays = new List<Ray>();
        List<Vector3> listOfRotations = new List<Vector3>() 
        { 
            Vector3.forward,
            new Vector3(1, 0, 1),
            Vector3.right,
            //new Vector3(1, 0, -1),
            //Vector3.back,
            //new Vector3(-1, 0, -1),
            Vector3.left,
            new Vector3(-1, 0, 1),
        };

        Vector3 position = transform.position + transform.TransformDirection(Vector3.up);
        foreach (var rotation in listOfRotations)
        {
            var rotationTemp = transform.TransformDirection(rotation);
            rotationTemp.y = 0;
            listOfRays.Add(new Ray(position, rotationTemp));
        }

        return listOfRays;
    }

    private void drawRays()
    {
        foreach (Ray singleRay in getListOfRays())
        {
            RaycastHit hit;
            if (Physics.Raycast(singleRay, out hit, timeoutDistance, ~(1 << LayerMask.NameToLayer("Ignore Raycast"))))
                Debug.DrawRay(singleRay.origin, singleRay.direction * hit.distance, Color.green);
            else
                Debug.DrawRay(singleRay.origin, singleRay.direction * timeoutDistance, Color.green);
        }
    }

    public List<float> getListOfHitsDistance()
    {
        List<float> hits = new List<float>();

        foreach (Ray singleRay in getListOfRays())
        {
            RaycastHit hit;
            if (Physics.Raycast(singleRay, out hit, timeoutDistance))
                hits.Add(hit.distance);
            else
                hits.Add(timeoutDistance);
        }

        return hits;
    }

    private string getDebugMessageDistance()
    {
        string message = "";
        foreach (float hit in getListOfHitsDistance())
        {
            message += hit.ToString() + ", ";
        }
        return (message.Substring(0, message.Length - 2));
    }
}
