using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    public AIType levelType; // Gán trong Inspector
    public Button button;

    void Start()
    {
        button.interactable = LevelUnlockManager.Instance.IsUnlocked(levelType);
    }
}