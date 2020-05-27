using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePlayerNameScript : MonoBehaviour
{
    private InputField _inputField;
    private string _playername;
    private const string PlayernameKey = "playername";
    private void Start()
    {
        _inputField = GetComponent<InputField>();
        
        _playername = PlayerPrefs.GetString(PlayernameKey);
        _inputField.text = _playername;
    }

    public void SavePlayerName()
    {
        _playername = _inputField.text;
        PlayerPrefs.SetString(PlayernameKey, _playername);
    }
}
