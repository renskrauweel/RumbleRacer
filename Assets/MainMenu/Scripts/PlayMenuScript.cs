using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuScript : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public RectTransform ParentPanel;
    
    void Start()
    {
        PrintScenes(GetScenes());
    }

    private string[] GetScenes()
    {
        var dir = Environment.CurrentDirectory + "\\Assets\\Scenes";
        var excludes = new List<string>
        {
            "MainMenu",
            "AITraining"
        };

        return Directory.GetFiles(dir, "*.unity").Select(Path.GetFileName).ToArray()
            .Select(scene => scene.Split('.')[0])
            .Where(newScene => !excludes.Contains(newScene))
            .ToArray();
    }

    private void PrintScenes(string[] scenes)
    {
        int x = 300;
        int y = -99;
        int z = 1;
        foreach (string scene in scenes)
        {
            GameObject go = Instantiate(ButtonPrefab, ParentPanel, false);
            go.transform.localScale = new Vector3(1, 1, 1);
            Button button = go.GetComponent<Button>();
            button.GetComponentInChildren<Text>().text = scene;
            button.GetComponent<RectTransform>().localPosition = new Vector3(x,y,z);
            button.onClick.AddListener(() =>
            {
                PlayerPrefs.SetString("currentcircuit", scene);
                SceneManager.LoadScene("Scenes/"+scene);
            });

            y -= 100;
        }
    }
}
