using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RayCasterScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 position = transform.position + transform.TransformDirection(Vector3.up);

        Ray mainRay = new Ray(position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(mainRay, out hit))
        {
            Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(position, transform.TransformDirection(Vector3.forward) * 1000, Color.green);
        }
    }
}
