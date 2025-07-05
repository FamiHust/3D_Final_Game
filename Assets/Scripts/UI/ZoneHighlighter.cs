// using UnityEngine;
// using UnityEngine.UI;

// public class ZoneHighlighter : MonoBehaviour
// {
//     public Image[] zones;
//     private Color defaultColor;

//     private void Start()
//     {
//         if (zones.Length > 0)
//             defaultColor = zones[0].color;
//     }

//     public void HighlightZones()
//     {
//         Color highlightColor = new Color32(255, 255, 255, 43);
//         foreach (var zone in zones)
//         {
//             zone.color = highlightColor;
//         }
//     }

//     public void ResetZones()
//     {
//         foreach (var zone in zones)
//         {
//             zone.color = defaultColor;
//         }
//     }
// }
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
        Color baseColor = new Color32(255, 255, 255, 35); // alpha thấp
        Color blinkColor = new Color32(255, 255, 255, 15); // alpha cao hơn để nhấp nháy
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
}
