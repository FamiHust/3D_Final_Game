using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    [Header("UI Elements")]
    public Text uiText;

    [Header("Typewriter Settings")]
    [TextArea]
    public string fullText;
    public float delay = 0.05f;

    private Coroutine typingCoroutine;

    void Start()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        uiText.text = "";
        foreach (char c in fullText)
        {
            uiText.text += c;
            if (c != ' ') // Không phát âm khi là dấu cách
            {
                SoundManager.PlaySound(SoundType.Typing, 1.2f);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    public void SetTextAndStart(string newText)
    {
        fullText = newText;
        StartTyping();
    }
}
