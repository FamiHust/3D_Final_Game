using UnityEngine;

public enum CursorType
{
    Default,
    Pointer,
    Attack,
    Settings
}

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    [Header("Cursor Textures")]
    public Texture2D defaultCursor;
    public Texture2D pointerCursor;
    public Texture2D attackCursor;
    public Texture2D settings;

    [SerializeField] private Vector2 hotspot = Vector2.zero;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetCursor(CursorType.Default);
    }

    public void SetCursor(CursorType type)
    {
        Texture2D cursorTexture = defaultCursor;

        switch (type)
        {
            case CursorType.Pointer:
                cursorTexture = pointerCursor;
                break;
            case CursorType.Attack:
                cursorTexture = attackCursor;
                break;
            case CursorType.Settings:
                cursorTexture = settings;
                break;
        }

        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }

    public void ResetToDefault()
    {
        SetCursor(CursorType.Default);
    }
}
