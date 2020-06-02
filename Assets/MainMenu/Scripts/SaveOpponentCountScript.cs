using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveOpponentCountScript : MonoBehaviour
{
    private InputField _inputField;
    private string _opponentCount;
    private const string OpponentCountKey = "opponentcount";
    private void Start()
    {
        _inputField = GetComponent<InputField>();
        
        _opponentCount = PlayerPrefs.GetString(OpponentCountKey);
        _inputField.text = _opponentCount;
    }

    public void SaveOpponentCount()
    {
        _opponentCount = _inputField.text;
        PlayerPrefs.SetString(OpponentCountKey, _opponentCount);
    }
}
