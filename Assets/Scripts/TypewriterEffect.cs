using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    [Header("UI Elements")]
    public Text uiText; // Text UI component

    [Header("Typewriter Settings")]
    [TextArea]
    public string fullText; // Văn bản đầy đủ cần hiển thị
    public float delay = 0.05f; // Thời gian trễ giữa các ký tự

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
            yield return new WaitForSeconds(delay);
        }
    }

    // Gọi hàm này nếu bạn muốn gán văn bản khác và chạy lại hiệu ứng
    public void SetTextAndStart(string newText)
    {
        fullText = newText;
        StartTyping();
    }
}
