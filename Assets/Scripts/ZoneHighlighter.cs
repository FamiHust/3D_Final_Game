using UnityEngine;
using UnityEngine.UI;

public class ZoneHighlighter : MonoBehaviour
{
    public Image[] zones;
    private Color defaultColor;

    private void Start()
    {
        if (zones.Length > 0)
            defaultColor = zones[0].color;
    }

    public void HighlightZones()
    {
        Color highlightColor = new Color32(255, 255, 255, 43);
        foreach (var zone in zones)
        {
            zone.color = highlightColor;
        }
    }

    public void ResetZones()
    {
        foreach (var zone in zones)
        {
            zone.color = defaultColor;
        }
    }
}


