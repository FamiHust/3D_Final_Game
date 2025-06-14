using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorHoverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CursorType cursorType = CursorType.Pointer;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance?.SetCursor(cursorType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance?.ResetToDefault();
    }
}
