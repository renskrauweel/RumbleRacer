using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float timer = 3f;
    private UIManagerScript UIManager;
    private Guid textGuid;
    private bool startCountdown = false;
    // Start is called before the first frame update
    void Start()
    {
        textGuid = new Guid();
        UIManager = gameObject.GetComponent<UIManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            if (GetComponent<CarControllerScript>().IsControllable())
            {
                GetComponent<CarControllerScript>().SetControllable(false);
            }

            timer -= Time.deltaTime;
            UIManager.DrawText(textGuid, Mathf.Ceil(timer).ToString(), 48, new UnityEngine.Color(245f / 255f, 147f / 255f, 66f / 255f), TextAnchor.MiddleCenter);
            if (timer < 0)
            {
                UIManager.RemoveText(textGuid);
                startCountdown = false;
                GetComponent<CarControllerScript>().SetControllable(true);
            }
        }

    }

    public void StartCountdown()
    {
        startCountdown = true;
    }
}
