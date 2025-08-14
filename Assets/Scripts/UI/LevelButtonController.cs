using UnityEngine;
using UnityEngine.UI;

public class LevelButtonController : MonoBehaviour
{
    public AIType levelType;
    public Button button;

    void Start()
    {
        button.interactable = LevelUnlockManager.Instance.IsUnlocked(levelType);
    }
}