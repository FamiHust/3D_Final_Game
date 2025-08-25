using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoneHighlighter : MonoBehaviour
{
    public Image[] zones;
    private Color defaultColor;
    private Coroutine blinkCoroutine;

    private void Start()
    {
        if (zones.Length > 0)
            defaultColor = zones[0].color;
    }

    public void HighlightZones()
    {
        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);
        
        blinkCoroutine = StartCoroutine(BlinkEffect());
    }

    public void HighlightZonesByElement(ElementType elementType)
    {
        if (blinkCoroutine != null)
            StopCoroutine(blinkCoroutine);
        
        blinkCoroutine = StartCoroutine(BlinkEffectByElement(elementType));
    }

    public void ResetZones()
    {
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        foreach (var zone in zones)
        {
            zone.color = defaultColor;
        }
    }

    private IEnumerator BlinkEffect()
    {
        Color baseColor = new Color32(255, 255, 255, 35);
        Color blinkColor = new Color32(255, 255, 255, 15); 
        float duration = 1f;
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime / duration;
            Color lerpedColor = Color.Lerp(baseColor, blinkColor, Mathf.PingPong(t, 1f));
            foreach (var zone in zones)
            {
                zone.color = lerpedColor;
            }
            yield return null;
        }
    }

    private IEnumerator BlinkEffectByElement(ElementType elementType)
    {
        Color baseColor = new Color32(255, 255, 255, 35);
        Color blinkColor = new Color32(255, 255, 255, 15); 
        float duration = 1f;
        float t = 0f;

        while (true)
        {
            t += Time.deltaTime / duration;
            Color lerpedColor = Color.Lerp(baseColor, blinkColor, Mathf.PingPong(t, 1f));
            
            // Chỉ highlight những zone có cùng element type
            for (int i = 0; i < zones.Length; i++)
            {
                var zoneElement = zones[i].GetComponent<ZoneElement>();
                if (zoneElement != null && zoneElement.elementType == elementType)
                {
                    zones[i].color = lerpedColor;
                }
                else
                {
                    zones[i].color = defaultColor;
                }
            }
            yield return null;
        }
    }
}
