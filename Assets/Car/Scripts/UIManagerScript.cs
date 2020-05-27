using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    private static Dictionary<Guid, GameObject> textItems = new Dictionary<Guid, GameObject>();
    private static Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        canvas.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

    }

    public void DrawText(Guid guid, string text, int fontSize, Color color, TextAnchor alignment)
    {
        if(canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>();
        }

        if (!textItems.ContainsKey(guid))
        {
            GameObject textParent = new GameObject(guid.ToString());
            textParent.AddComponent<RectTransform>();
            textParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            textParent.transform.SetParent(canvas.transform);

            Text textItem = textParent.gameObject.AddComponent<Text>();
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            textItem.font = ArialFont;
            textItem.material = ArialFont.material;
            textItem.fontStyle = FontStyle.Bold;
            textItems.Add(guid, textParent);
        }

        if (textItems.ContainsKey(guid))
        {
            textItems[guid].GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
            textItems[guid].GetComponent<Text>().fontSize = fontSize;
            textItems[guid].GetComponent<Text>().alignment = alignment;
            textItems[guid].GetComponent<Text>().color = color;
            textItems[guid].GetComponent<Text>().text = text;
        }
    }

    public void DrawText(Guid guid, string text, int fontSize, Color color, TextAnchor alignment, Vector2 position)
    {
        if (canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>();
        }

        if (!textItems.ContainsKey(guid))
        {
            GameObject textParent = new GameObject(guid.ToString());
            textParent.AddComponent<RectTransform>();
            textParent.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            textParent.transform.SetParent(canvas.transform);

            Text textItem = textParent.gameObject.AddComponent<Text>();
            Font ArialFont = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
            textItem.font = ArialFont;
            textItem.material = ArialFont.material;
            textItem.fontStyle = FontStyle.Bold;
            textItems.Add(guid, textParent);
        }

        if (textItems.ContainsKey(guid))
        {
            textItems[guid].GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
            textItems[guid].GetComponent<Text>().fontSize = fontSize;
            textItems[guid].GetComponent<Text>().rectTransform.anchoredPosition = position;
            textItems[guid].GetComponent<Text>().alignment = alignment;
            textItems[guid].GetComponent<Text>().color = color;
            textItems[guid].GetComponent<Text>().text = text;
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
