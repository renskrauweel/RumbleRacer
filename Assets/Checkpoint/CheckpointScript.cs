using System.Linq;
using System.IO;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class CheckpointScript : MonoBehaviour
{
    public int order;
    public int circuitNumber;
    public Material Active;
    public Material Inactive;
    private string highscoreFileDestination;
    private string playername;

    private void Start()
    {
        highscoreFileDestination = Application.persistentDataPath + "/highscores.dat";
        playername = PlayerPrefs.GetString("playername");
        if (order != 0) setActiveMaterial(false);     
    }

    private void OnTriggerEnter(Collider other)
    {
        CarRaceTimeScript carRaceTimeScript = other.gameObject.GetComponentInParent<CarRaceTimeScript>();

        if (order == 0 && carRaceTimeScript.GetCompletedLap())
        {
            carRaceTimeScript.AddCheckpointHit();
            carRaceTimeScript.AddCompleteLap();
            if (carRaceTimeScript.GetCompletedRace()) {
                SaveHighscore(carRaceTimeScript.GetTotalRaceTime());
                Debug.LogWarning("Finished the race in " + carRaceTimeScript.GetTotalRaceTime() + " Seconds!");
            }
        }
        if (carRaceTimeScript.GetCheckpointsHit() == order)
        {
            carRaceTimeScript.AddCheckpointHit();
            if (other.tag == "Player")
            {
                setActiveMaterial(false);
                int nextActive = carRaceTimeScript.GetCheckpointsHit() < carRaceTimeScript.GetTotalCheckpointCount() ? order + 1 : 0;
                GameObject.FindGameObjectsWithTag("Checkpoint").Where(x => x.GetComponent<CheckpointScript>().order == nextActive && x.GetComponent<CheckpointScript>().circuitNumber == this.circuitNumber).First().GetComponent<CheckpointScript>().setActiveMaterial(true);
            }
        }
    }

    private void setActiveMaterial(bool activate)
    {
        GetComponent<Renderer>().material = activate ? Active : Inactive;
    }

    private void SaveHighscore(float endTime)
    {
        if (playername.Length == 0) return;

        if (!File.Exists(highscoreFileDestination)) File.Create(highscoreFileDestination);

        using (StreamWriter sw = File.AppendText(highscoreFileDestination))
        {
            sw.WriteLine(playername + "-" + endTime + "Seconds");
        }
    }
}
