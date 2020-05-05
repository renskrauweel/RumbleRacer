using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float timer = 3f;
    public Text countdownText;
    private bool startCountdown = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startCountdown)
        {
            if (GetComponentInParent<CarControllerScript>().IsControllable())
            {
                GetComponentInParent<CarControllerScript>().SetControllable(false);
            }

            timer -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(timer).ToString();
            if (timer < 0)
            {
                countdownText.text = "";
                startCountdown = false;
                GetComponentInParent<CarControllerScript>().SetControllable(true);
            }
        }

    }

    public void StartCountdown()
    {
        startCountdown = true;
    }
}
