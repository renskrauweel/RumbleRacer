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
            timer -= Time.deltaTime;

            switch (Mathf.Ceil(timer))
            {
                case 3:
                    countdownText.text = Mathf.Ceil(timer).ToString();
                    break;
                case 2:
                    countdownText.text = Mathf.Ceil(timer).ToString();
                    break;
                case 1:
                    countdownText.text = Mathf.Ceil(timer).ToString();
                    break;
                case 0:
                    countdownText.text = "GO";
                    break;
                default:
                    countdownText.text = "";
                    startCountdown = false;
                    break;
            }
        }

    }

    public void StartCountdown()
    {
        startCountdown = true;
    }
}
