using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FollowObjectScript : MonoBehaviour
{
    private GameObject[] cars;
    public string GoalTag = "Car";
    public float minimalDistance = 25;

    // Start is called before the first frame update
    void Start()
    {
        cars = GameObject.FindGameObjectsWithTag(GoalTag);
    }

    // Update is called once per frame
    void Update()
    {
        if(cars.Length != 0)
        {
            int closestCar = 0;
            float closestCarDistance = 0;

            for (int i = 0; i < cars.Length; i++)
            {
                float currentCarDistance = Vector3.Distance(transform.position, cars[i].transform.position);
                if (currentCarDistance <= closestCarDistance || i == 0)
                {
                    closestCar = i;
                    closestCarDistance = currentCarDistance;
                }
            }

            transform.LookAt(cars[closestCar].transform);
        }       
    }
}
