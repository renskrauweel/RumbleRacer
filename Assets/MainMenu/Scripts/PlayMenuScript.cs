using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuScript : MonoBehaviour
{
    public GameObject ButtonPrefab;
    public RectTransform ParentPanel;
    public Dropdown DropdownGhosts;
    public Toggle ToggleCreateGhost;
    private List<string> Scenes = new List<string> // Set scene names here because EditorBuildSettings.scenes not available at build
    {
        "Oval",
        "Go-Kart",
        "Suzuka"
    };
    
    void Start()
    {
        // Reset playerprefs
        PlayerPrefs.SetString("ghostfile", "No ghost");
        PlayerPrefs.SetInt("createghost", 0);
        
        DropdownGhosts.AddOptions(GetGhostFiles());
        PrintScenes(GetScenes());
    }

    private string[] GetScenes()
    {
        return Scenes.ToArray();
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

    private List<string> GetGhostFiles()
    {
        if (!Directory.Exists(Constants.LOG_FOLDER_REPLAY)) Directory.CreateDirectory(Constants.LOG_FOLDER_REPLAY);
        return Directory.GetFiles(Constants.LOG_FOLDER_REPLAY).Select(Path.GetFileName).ToList();
    }

    public void OnDropdownGhostChange()
    {
        string value = DropdownGhosts.options[DropdownGhosts.value].text;
        if (value != "No ghost")
        {
            PlayerPrefs.SetString("ghostfile", Constants.LOG_FOLDER_REPLAY+value);
        }
        else
        {
            PlayerPrefs.SetString("ghostfile", value);
        }
    }

    public void OnCreateGhostCheckboxChange()
    {
        PlayerPrefs.SetInt("createghost", ToggleCreateGhost.isOn ? 1 : 0);
    }
}
