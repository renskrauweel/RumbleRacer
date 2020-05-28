using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectScript : MonoBehaviour
{
    private List<GameObject> objects = new List<GameObject>();
    public string GoalTags = "Car, CarAI";
    public float minimalDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        string[] tags = GoalTags.Split(new[] { ", " }, StringSplitOptions.None);
        foreach (var tag in tags)
        {
            foreach (var temp in GameObject.FindGameObjectsWithTag(tag)){
                objects.Add(temp);
            }
                
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(objects.Count != 0)
        {
            int closestObject = 0;
            float closestObjectDistance = 0;

            for (int i = 0; i < objects.Count; i++)
            {
                float currentObjectDistance = Vector3.Distance(transform.position, objects[i].transform.position);
                if (currentObjectDistance <= closestObjectDistance || i == 0)
                {
                    closestObject = i;
                    closestObjectDistance = currentObjectDistance;
                }
            }

            transform.LookAt(objects[closestObject].transform);
        }       
    }
}
