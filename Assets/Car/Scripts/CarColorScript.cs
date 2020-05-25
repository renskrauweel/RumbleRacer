using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.GameCenter;

public class CarColorScript : MonoBehaviour
{
    private Material material;
    private void Start()
    {
        material = GameObject.FindGameObjectWithTag("CarBody").GetComponent<Renderer>().material;
    }

    public void SetRandomColor()
    {
        material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void SetColor(Color color)
    {
        material.color = color;
    }

    public void SetColorRGB(float r, float g, float b)
    {
        material.color = new Color(r, g, b);
    }
}
