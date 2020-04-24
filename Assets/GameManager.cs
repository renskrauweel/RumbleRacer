using System;
using Lib.Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool StartAiStream = false;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StartAiStream) DoStartAiStream();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DoStartAiStream()
    {
        try
        {
            StreamService streamService = new StreamService();
            streamService.StartStream("Rens is cool");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
