using System;
using System.Linq;
using System.Threading.Tasks;
using Lib.Services;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool StartAiStream = false;
    private bool countdown = true;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StartAiStream) DoStartAiStream();
        if (countdown) StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private async Task StartCountdown()
    {
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach(car => car.GetComponent<CarControllerScript>().SetControllable(false));
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach(car => car.GetComponentInChildren<CountdownScript>().StartCountdown());
        await Task.Delay(3000);
        GameObject.FindGameObjectsWithTag("Car").ToList().ForEach(car => car.GetComponent<CarControllerScript>().SetControllable(true));
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
