using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FinishLineScript : MonoBehaviour
{
    private CarRaceTimeScript carRaceTimeScript;
    private string highscoreFileDestination;
    private string playername;

    private void Start()
    {
        highscoreFileDestination = Application.persistentDataPath + "/highscores.dat";
        playername = PlayerPrefs.GetString("playername");
    }

    private void OnTriggerEnter(Collider other)
    {
        carRaceTimeScript = other.gameObject.GetComponentInParent<CarRaceTimeScript>();

        if (!carRaceTimeScript.GetHitFinishLine() && carRaceTimeScript.HasHitAllCheckPoints())
        {
            carRaceTimeScript.SetCurrentRaceTime();
            carRaceTimeScript.SetHitFinishLine(true);
            float endTime = carRaceTimeScript.GetCurrentRaceTimeMs();
            SaveHighscore(endTime);
            Debug.Log("Finished in "+endTime+"MS");
        } else if (!carRaceTimeScript.HasHitAllCheckPoints())
        {
            Debug.Log("You haven't hit all checkpoints!");
        }
    }

    private void SaveHighscore(float endTime)
    {
        if (playername.Length == 0) return;
        
        // Open file
        FileStream file = File.Exists(highscoreFileDestination) ? File.OpenWrite(highscoreFileDestination) : File.Create(highscoreFileDestination);

        // Write
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, playername + "-" + endTime +"MS");
        
        // Close
        file.Close();
    }
}
