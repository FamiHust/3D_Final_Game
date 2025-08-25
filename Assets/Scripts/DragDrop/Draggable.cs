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
    private Vector3 pointerPosition; // world-space target while dragging
    private Vector2 lastPointerPos;

    [SerializeField] private float dragSwayAmplitude = 60f; // pixels (max lateral offset)
    [SerializeField] private float dragSwayResponse = 1f; // how strongly dx maps to sway
    [SerializeField] private float dragTiltMaxAngle = 45f; // degrees
    [SerializeField] private float dragTiltSensitivity = 0.6f; // angle per px/frame
    [SerializeField] private float positionSmoothTime = 0.06f; // seconds
    [SerializeField] private float rotationSmoothTime = 0.06f; // seconds
    [SerializeField] private float swaySmoothTime = 0.08f; // seconds for lateral sway

    private Vector3 positionVelocity; // for SmoothDamp
    private float rotationVelocity;   // for SmoothDampAngle
    private float currentSway;        // current lateral offset
    private float swayVelocity;       // for SmoothDamp of sway
    private Sequence returnSeq;       // tween sequence for returning to hand

    private void Start()
    {
        originalScale = transform.localScale;
        zoneHighlighter = FindObjectOfType<ZoneHighlighter>();
        pointerPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        pointerPosition = eventData.position;
        lastPointerPos = eventData.position;
        positionVelocity = Vector3.zero;
        rotationVelocity = 0f;
        currentSway = 0f;
        swayVelocity = 0f;
        // Hủy tween trả về nếu còn tồn tại
        if (returnSeq != null)
        {
            returnSeq.Kill();
            returnSeq = null;
        }
        currentSway = 0f;
        swayVelocity = 0f;

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

        // Highlight zone theo element type của thẻ đang được kéo
        var thisCard = GetComponent<ThisCard>();
        if (thisCard != null && thisCard.thisCard.Count > 0)
        {
            zoneHighlighter.HighlightZonesByElement(thisCard.thisCard[0].elementType);
            
            // Bắt đầu vẽ attack line nếu thẻ có thể tấn công
            if (thisCard.canAttack && thisCard.summoned)
            {
                Debug.Log($"[Draggable] Starting attack line for card: {thisCard.cardName}, canAttack: {thisCard.canAttack}, summoned: {thisCard.summoned}");
                StartCoroutine(ShowAttackLineWhileDragging(thisCard));
            }
            else
            {
                Debug.Log($"[Draggable] Card cannot show attack line: {thisCard.cardName}, canAttack: {thisCard.canAttack}, summoned: {thisCard.summoned}");
            }
        }
        else
        {
            zoneHighlighter.HighlightZones();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Lưu vị trí trỏ chuột, di chuyển thật được xử lý mượt trong Update()
        pointerPosition = eventData.position;

        if (placeHolder.transform.parent != placeHolderParent)
            placeHolder.transform.SetParent(placeHolderParent);

        int newSibingIndex = placeHolderParent.childCount;

        for (int i = 0; i < placeHolderParent.childCount; i++)
        {
            // Dùng vị trí con trỏ để tránh index giật do hiệu ứng lắc ngang
            if (eventData.position.x < placeHolderParent.GetChild(i).position.x)
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
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        positionVelocity = Vector3.zero;
        rotationVelocity = 0f;
        currentSway = 0f;
        swayVelocity = 0f;

        // Nếu thả vào Zone (có ZoneElement) thì đặt ngay lập tức để không delay Summon
        bool droppingToZone = parentToReturnTo != null && parentToReturnTo.GetComponent<ZoneElement>() != null;
        if (droppingToZone)
        {
            if (returnSeq != null) { returnSeq.Kill(); returnSeq = null; }
            Transform destParent = parentToReturnTo;
            int targetIndex = placeHolder != null ? placeHolder.transform.GetSiblingIndex() : (destParent != null ? destParent.childCount : 0);
            if (destParent != null && destParent.childCount > 0)
                targetIndex = Mathf.Clamp(targetIndex, 0, destParent.childCount);

            transform.SetParent(destParent, worldPositionStays: false);
            transform.SetSiblingIndex(Mathf.Clamp(targetIndex, 0, destParent.childCount - 1));
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.DOScale(originalScale, 0.1f).SetEase(Ease.OutBack);

            if (placeHolder != null)
            {
                Destroy(placeHolder);
            }
            zoneHighlighter.ResetZones();
            
            // Nếu là bài spell TẤN CÔNG, bật effect chỉ mục tiêu ngay lập tức
            var thisCard = GetComponent<ThisCard>();
            if (thisCard != null && thisCard.spell && thisCard.dealDamage)
            {
                var enemyBoardWatcher = FindObjectOfType<EnemyBoardWatcher>();
                if (enemyBoardWatcher != null)
                {
                    enemyBoardWatcher.ShowTargetEffectDirectly();
                }
            }
        }
        else
        {
            // Tính toán điểm đến theo world position của placeholder để tween khi vẫn nằm ngoài layout
            Transform destParent = parentToReturnTo;
            int targetIndex = placeHolder != null ? placeHolder.transform.GetSiblingIndex() : (destParent != null ? destParent.childCount : 0);
            if (destParent != null && destParent.childCount > 0)
                targetIndex = Mathf.Clamp(targetIndex, 0, destParent.childCount);
            Vector3 destWorldPos = placeHolder != null ? placeHolder.transform.position : (destParent != null ? destParent.position : transform.position);

            // Tween theo world pos, sau đó mới gán parent để tránh xung đột với Layout
            if (returnSeq != null) { returnSeq.Kill(); returnSeq = null; }
            returnSeq = DOTween.Sequence();
            returnSeq.Join(transform.DOMove(destWorldPos, 0.25f).SetEase(Ease.OutQuad));
            returnSeq.Join(transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.OutQuad));
            returnSeq.Join(transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutBack));
            returnSeq.OnComplete(() =>
            {
                if (destParent != null)
                {
                    transform.SetParent(destParent, worldPositionStays: false);
                    transform.SetSiblingIndex(Mathf.Clamp(targetIndex, 0, destParent.childCount - 1));
                }
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                if (placeHolder != null)
                {
                    Destroy(placeHolder);
                }
                    zoneHighlighter.ResetZones();
                returnSeq = null;
            });
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Chỉ hover effect khi thẻ có thể được kéo
        if (!isDragging && CanBeDragged())
        {
            transform.DOScale(originalScale * 1.1f, 0.15f).SetEase(Ease.OutBack);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Chỉ hover effect khi thẻ có thể được kéo
        if (!isDragging && CanBeDragged())
        {
            transform.DOScale(originalScale, 0.15f).SetEase(Ease.InBack);
        }
    }

    private bool CanBeDragged()
    {
        // Kiểm tra xem thẻ có thể được kéo không
        var thisCard = GetComponent<ThisCard>();
        if (thisCard != null)
        {
            return thisCard.canBeSummon;
        }
        return this.enabled; // Fallback về trạng thái enable của component
    }

    public void ForceEndDrag()
    {
        if (!isDragging) return;

        isDragging = false;
        
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        positionVelocity = Vector3.zero;
        rotationVelocity = 0f;
        currentSway = 0f;
        swayVelocity = 0f;

        // Tween theo world pos đến chỗ placeholder rồi mới set parent
        Transform destParent = parentToReturnTo != null ? parentToReturnTo : transform.parent;
        int targetIndex = placeHolder != null ? placeHolder.transform.GetSiblingIndex() : (destParent != null ? destParent.childCount : 0);
        if (destParent != null && destParent.childCount > 0)
            targetIndex = Mathf.Clamp(targetIndex, 0, destParent.childCount);
        Vector3 destWorldPos = placeHolder != null ? placeHolder.transform.position : (destParent != null ? destParent.position : transform.position);

        if (returnSeq != null) { returnSeq.Kill(); returnSeq = null; }
        returnSeq = DOTween.Sequence();
        returnSeq.Join(transform.DOMove(destWorldPos, 0.25f).SetEase(Ease.OutQuad));
        returnSeq.Join(transform.DOLocalRotate(Vector3.zero, 0.2f).SetEase(Ease.OutQuad));
        returnSeq.Join(transform.DOScale(originalScale, 0.2f).SetEase(Ease.OutBack));
        returnSeq.OnComplete(() =>
        {
            if (destParent != null)
            {
                transform.SetParent(destParent, worldPositionStays: false);
                transform.SetSiblingIndex(Mathf.Clamp(targetIndex, 0, destParent.childCount - 1));
            }
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            if (placeHolder != null)
            {
                Destroy(placeHolder);
            }
            zoneHighlighter?.ResetZones();
            returnSeq = null;
        });
    }

    // Hiển thị attack line khi kéo lá bài tấn công
    private IEnumerator ShowAttackLineWhileDragging(ThisCard thisCard)
    {
        Debug.Log("[Draggable] ShowAttackLineWhileDragging started");
        
        GameObject attackLine = null;
        RectTransform lineRect = null;
        GameObject finalTarget = null;
        RectTransform finalTargetRt = null;
        
        // Tạo attack line object
        if (SimpleParticleManager.Instance != null)
        {
            attackLine = new GameObject("AttackLineWhileDragging");
            lineRect = attackLine.AddComponent<RectTransform>();
            lineRect.SetParent(transform.parent);
            lineRect.sizeDelta = Vector2.one;
            Debug.Log("[Draggable] Attack line object created successfully");
        }
        else
        {
            Debug.LogError("[Draggable] SimpleParticleManager.Instance is null!");
        }
        
        // Tìm target cuối cùng ngay từ đầu
        Transform finalTargetTransform = FindNearestTarget(thisCard);
        Debug.Log($"[Draggable] Final target found: {(finalTargetTransform != null ? finalTargetTransform.name : "null")}");
        
        if (finalTargetTransform != null)
        {
            finalTarget = new GameObject("FinalTarget");
            finalTargetRt = finalTarget.AddComponent<RectTransform>();
            finalTargetRt.SetParent(transform.parent);
            finalTargetRt.sizeDelta = Vector2.one;
            
            // Đặt vị trí target cuối cùng
            Vector3 targetWorldPos = finalTargetTransform.position;
            Vector3 targetScreenPos = Camera.main.WorldToScreenPoint(targetWorldPos);
            Vector2 targetCanvasPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent as RectTransform,
                targetScreenPos,
                Camera.main,
                out targetCanvasPos
            );
            finalTargetRt.localPosition = targetCanvasPos;
            Debug.Log($"[Draggable] Final target position set: {targetCanvasPos}");
        }
        
        Debug.Log("[Draggable] Starting drag loop...");
        while (isDragging && thisCard != null)
        {
            if (attackLine != null && lineRect != null)
            {
                // Cập nhật vị trí của line theo chuột (real-time)
                Vector2 mouseCanvasPos = Vector2.zero;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    transform.parent as RectTransform,
                    pointerPosition,
                    Camera.main,
                    out mouseCanvasPos
                );
                
                lineRect.localPosition = mouseCanvasPos;
                
                // Vẽ line từ lá bài đến chuột (real-time)
                var fromRt = transform as RectTransform;
                if (fromRt != null && lineRect != null)
                {
                    // Xóa line cũ và vẽ line mới với duration ngắn để mượt
                    if (SimpleParticleManager.Instance != null)
                    {
                        SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, lineRect, 6f, 26f, 16f, 36f, 0.05f, Color.white, 180f);
                        Debug.Log($"[Draggable] Drawing line from {fromRt.position} to {mouseCanvasPos}");
                    }
                }
            }
            else
            {
                Debug.LogWarning("[Draggable] Attack line or lineRect is null!");
            }
            
            yield return new WaitForSeconds(0.02f); // Cập nhật mỗi 0.02s để mượt hơn
        }
        Debug.Log("[Draggable] Drag loop ended");
        
        // Khi thả ra, vẽ line cuối cùng đến target
        Debug.Log("[Draggable] Drawing final line to target...");
        if (finalTarget != null && finalTargetRt != null && SimpleParticleManager.Instance != null)
        {
            var fromRt = transform as RectTransform;
            if (fromRt != null)
            {
                // Vẽ line cuối cùng với duration dài hơn để đẹp
                                        SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, finalTargetRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                Debug.Log($"[Draggable] Final line drawn from {fromRt.position} to {finalTargetRt.position}");
                
                // Xóa target cuối cùng sau khi line hoàn thành
                Destroy(finalTarget, 0.7f);
            }
        }
        else
        {
            Debug.LogWarning("[Draggable] Cannot draw final line - missing components");
        }
        
        // Xóa attack line khi kết thúc
        if (attackLine != null)
        {
            Destroy(attackLine);
        }
    }
    
    // Tìm target gần nhất để tấn công
    private Transform FindNearestTarget(ThisCard thisCard)
    {
        Transform nearestTarget = null;
        float nearestDistance = float.MaxValue;
        
        // Kiểm tra xem có phải là AI tấn công Player không
        bool isAIAttackingPlayer = thisCard.transform.parent != null && 
                                  thisCard.transform.parent.name.StartsWith("Enemy_Zone");
        
        if (isAIAttackingPlayer)
        {
            // AI tấn công Player: tìm Player model hoặc Player cards
            var playerHp = GameObject.Find("Health_Bar")?.GetComponent<PlayerHp>();
            if (playerHp != null && playerHp.PlayerModel != null)
            {
                float distance = Vector3.Distance(thisCard.transform.position, playerHp.PlayerModel.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = playerHp.PlayerModel;
                }
            }
            
            // Tìm Player cards trong battle zones
            for (int i = 0; i < 8; i++)
            {
                string zoneName = i == 0 ? "Zone" : "Zone" + i;
                GameObject zone = GameObject.Find(zoneName);
                if (zone != null && zone.transform.childCount > 0)
                {
                    var playerCard = zone.transform.GetChild(0).GetComponent<ThisCard>();
                    if (playerCard != null && playerCard.summoned)
                    {
                        float distance = Vector3.Distance(thisCard.transform.position, playerCard.transform.position);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestTarget = playerCard.transform;
                        }
                    }
                }
            }
        }
        else
        {
            // Player tấn công AI: tìm AI model hoặc AI cards
            var enemyHp = GameObject.Find("Health_Bar")?.GetComponent<EnemyHp>();
            if (enemyHp != null && enemyHp.EnemyModel != null)
            {
                float distance = Vector3.Distance(thisCard.transform.position, enemyHp.EnemyModel.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestTarget = enemyHp.EnemyModel;
                }
            }
            
            // Tìm AI cards trong enemy zones
            for (int i = 0; i < 8; i++)
            {
                string zoneName = i == 0 ? "Enemy_Zone" : "Enemy_Zone" + i;
                GameObject zone = GameObject.Find(zoneName);
                if (zone != null && zone.transform.childCount > 0)
                {
                    var aiCard = zone.transform.GetChild(0).GetComponent<AICardToHand>();
                    if (aiCard != null && aiCard.isSummoned)
                    {
                        float distance = Vector3.Distance(thisCard.transform.position, aiCard.transform.position);
                        if (distance < nearestDistance)
                        {
                            nearestDistance = distance;
                            nearestTarget = aiCard.transform;
                        }
                    }
                }
            }
        }
        
        return nearestTarget;
    }
    
    private void Update()
    {
        if (!isDragging) return;

        float dx = ((Vector2)pointerPosition).x - lastPointerPos.x;
        float targetSway = Mathf.Clamp(dx * dragSwayResponse, -dragSwayAmplitude, dragSwayAmplitude);
        currentSway = Mathf.SmoothDamp(currentSway, targetSway, ref swayVelocity, swaySmoothTime);
        Vector3 targetPos = pointerPosition + new Vector3(currentSway, 0f, 0f);
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref positionVelocity, positionSmoothTime);

        float targetTilt = Mathf.Clamp(dx * dragTiltSensitivity, -dragTiltMaxAngle, dragTiltMaxAngle);
        float currentZ = transform.localEulerAngles.z;
        float newZ = Mathf.SmoothDampAngle(currentZ, -targetTilt, ref rotationVelocity, rotationSmoothTime);
        transform.localEulerAngles = new Vector3(0f, 0f, newZ);

        lastPointerPos = pointerPosition;
    }
} 