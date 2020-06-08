using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    private float timer = 3f;
    private UIManagerScript UIManager;
    private Guid textGuid;
    private bool countdown = false;
    // Start is called before the first frame update
    void Start()
    {
        textGuid = new Guid();
        UIManager = gameObject.GetComponent<UIManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (countdown)
        {
            GetComponent<CarControllerScript>().SetControllable(false);
            timer -= Time.deltaTime;
            if(gameObject.CompareTag("Car"))
                if(Mathf.Ceil(timer) > 2)
                    UIManager.DrawText(textGuid, Mathf.Ceil(timer).ToString(), 48, new UnityEngine.Color(200/255f, 40/255f, 0/255f), TextAnchor.LowerLeft, new Vector2(Screen.width / 2, Screen.height / 2));       
                else if(Mathf.Ceil(timer) > 1)
                    UIManager.DrawText(textGuid, Mathf.Ceil(timer).ToString(), 48, new UnityEngine.Color(215 / 255f, 90/ 255f, 0 / 255f), TextAnchor.LowerLeft, new Vector2(Screen.width / 2, Screen.height / 2));
                else if (Mathf.Ceil(timer) > 0)
                    UIManager.DrawText(textGuid, Mathf.Ceil(timer).ToString(), 48, new UnityEngine.Color(100 / 255f, 175 / 255f, 0 / 255f), TextAnchor.LowerLeft, new Vector2(Screen.width / 2, Screen.height / 2));

            if (timer < 0)
            {
                if (gameObject.CompareTag("Car"))
                    UIManager.RemoveText(textGuid);
                countdown = false;
                GetComponent<CarControllerScript>().SetControllable(true);
            }
        }
    }

    public void StartCountdown()
    {
        countdown = true;
    }
}
