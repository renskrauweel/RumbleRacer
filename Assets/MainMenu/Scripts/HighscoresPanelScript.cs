using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Lib.Highscores;
using UnityEngine;
using UnityEngine.UI;

public class HighscoresPanelScript : MonoBehaviour
{
    private List<Highscore> _highscores;
    public Dropdown _dropdownCircuits;
    private readonly string _highscoresTag = "TxtHighscore";

    void Start()
    {
        _highscores = new List<Highscore>();

        LoadHighscores();
        PrintHighscores(_dropdownCircuits.options[_dropdownCircuits.value].text);
    }

    private void LoadHighscores()
    {
        string destination = Application.persistentDataPath + "/highscores.dat";

        using (StreamReader sr = new StreamReader(destination))
        {
            string data = sr.ReadToEnd();
            foreach (string dataRow in data.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                string[] dataSplitted = dataRow.Split('-');
                if(dataSplitted.Length == 3) _highscores.Add(new Highscore(dataSplitted[0], 
                    dataSplitted[1], Math.Round(double.Parse(dataSplitted[2]
                        .Replace("Seconds", "")), 3, MidpointRounding.ToEven)));
            }
        }
    }

    private void PrintHighscores(string circuitName)
    {
        ClearHighScores();
        
        int x = -600;
        int y = 300;
        int z = 0;
        int count = 0;
        
        List<Highscore> highscoresToUse = _highscores.OrderBy(h => h.Time).Where(h => h.Circuit == circuitName).ToList();
        if (highscoresToUse.Count > 39) highscoresToUse = highscoresToUse.GetRange(0, 39);
        
        foreach (Highscore highscore in highscoresToUse)
        {
            GameObject newText = new GameObject("TxtHighscore", typeof(RectTransform));
            newText.tag = _highscoresTag;
            Text textComponent = newText.AddComponent<Text>();
            textComponent.text = count+1 + ". " + highscore.AsHumanFormat();
            textComponent.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            textComponent.color = Color.white;
            textComponent.alignment = TextAnchor.MiddleCenter;
            textComponent.fontSize = 42;
            textComponent.horizontalOverflow = HorizontalWrapMode.Overflow;

            newText.transform.SetParent(transform);
            newText.GetComponent<RectTransform>().localPosition = new Vector3(x,y,z);
            newText.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);

            y -= 50;
            
            count++;
            if (count == 13)
            {
                x = 0;
                y = 300;
            } else if (count == 26)
            {
                x = 600;
                y = 300;
            }
        }
    }

    private void ClearHighScores()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag(_highscoresTag)) Destroy(o);
    }

    public void OnValueChanged()
    {
        PrintHighscores(_dropdownCircuits.options[_dropdownCircuits.value].text);
    }
}
