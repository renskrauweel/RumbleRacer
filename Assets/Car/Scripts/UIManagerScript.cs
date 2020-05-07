using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class UIManagerScript : MonoBehaviour
{
    private static Dictionary<Guid,Text> textItems = new Dictionary<Guid, Text>();
    private static Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawText(Guid guid, string text, int fontSize, Color color, TextAnchor alignment)
    {
        if (!textItems.ContainsKey(guid))
        {
            Text textItem = canvas.gameObject.AddComponent<Text>();
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            textItem.font = ArialFont;
            textItem.material = ArialFont.material;
            textItem.fontStyle = FontStyle.Bold;
            textItems.Add(guid, textItem);
        }

        if (textItems.ContainsKey(guid))
        {
            textItems[guid].alignment = alignment;
            textItems[guid].fontSize = fontSize;
            textItems[guid].color = color;
            textItems[guid].text = text;
        }
    }

    public void RemoveText(Guid guid)
    {
        if (textItems.ContainsKey(guid))
        {
            Destroy(textItems[guid]);
            textItems.Remove(guid);
        }
        else
        {
            Debug.LogError("Guid: " + guid + " does not exist");
        }
    }
}
