using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; 

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform parentToReturnTo = null;
    public Transform placeHolderParent = null;

    private GameObject placeHolder = null;
    private Vector3 originalScale;
    
    private bool isDragging = false;
    private ZoneHighlighter zoneHighlighter;

    private void Start()
    {
        originalScale = transform.localScale;
        zoneHighlighter = FindObjectOfType<ZoneHighlighter>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        placeHolder = new GameObject();
        placeHolder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeHolder.AddComponent<LayoutElement>();
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        parentToReturnTo = this.transform.parent;
        placeHolderParent = parentToReturnTo;
        this.transform.SetParent(this.transform.parent.parent);

        GetComponent<CanvasGroup>().blocksRaycasts = false;

        zoneHighlighter.HighlightZones();
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);

        int newSibingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            if (this.transform.position.x < placeHolderParent.GetChild(i).position.x)
            {
                newSibingIndex = i;

                if (placeHolder.transform.GetSiblingIndex() < newSibingIndex)
                    newSibingIndex--;
                break;
            }
        }

        placeHolder.transform.SetSiblingIndex(newSibingIndex);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        this.transform.SetParent(parentToReturnTo);
        this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutBack);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Destroy(placeHolder);
        zoneHighlighter.ResetZones();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isDragging)
        {
            transform.DOScale(originalScale * 1.1f, 0.15f).SetEase(Ease.OutBack);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isDragging)
        {
            transform.DOScale(originalScale, 0.15f).SetEase(Ease.InBack);
        }
    }
} 