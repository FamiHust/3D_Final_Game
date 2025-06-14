using UnityEngine;
using TMPro;

public class DisplayUsername : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UsernameText;

    void Start()
    {
        string username = PlayerPrefs.GetString("Username", "Guest");
        UsernameText.text = username.ToString();
    }
}
