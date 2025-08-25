using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AICardToHand : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();

    public int thisID;
    public int id;
    public string cardName;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Sprite thisSprite;
    [SerializeField] private Image thatImage;
    [SerializeField] private Image healthBarImage;
    [SerializeField] private GameObject HealthBar;

    public static int DrawX;
    public int drawXcards;
    public int addXmaxMana;

    public int hurted;
    public int actualpower;
    public int returnXcards;
    public int damageReceived; // Sát thương nhận được khi bị tấn công

    public GameObject Hand;
    public GameObject It;
    public GameObject Graveyard;
    public GameObject cardBack;
    public GameObject[] AiZones = new GameObject[8];
    public GameObject[] battleZones = new GameObject[8];

    public int z = 0;
    public int numberOfCardsInDeck;
    public int healXpower;

    public bool isTarget;
    public bool thisCardCanBeDestroyed;
    public bool canAttack;
    public bool summoningSickness;
    public bool isSummoned;

    public AIEffect aiEffect;
    
    private Color originalCardColor;

    void Start()
    {
        thisCard[0] = CardDatabase.cardList[thisID];
        numberOfCardsInDeck = AI.deckSize;

        Hand = GameObject.Find("Enemy_Hand");
        z = 0;

        Graveyard = GameObject.Find("Enemy_Graveyard");
        StartCoroutine(AfterVoidStart());
        
        // Lưu màu gốc của thẻ
        if (thatImage != null)
        {
            originalCardColor = thatImage.color;
        }

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                AiZones[i] = GameObject.Find("Enemy_Zone");
            else
                AiZones[i] = GameObject.Find("Enemy_Zone" + i);
        }

        summoningSickness = true;
        damageReceived = 0;

        for (int i = 0; i < 8; i++)
        {
            if (i == 0)
                battleZones[i] = GameObject.Find("Enemy_Zone");
            else
                battleZones[i] = GameObject.Find("Enemy_Zone" + i);
        }
    }

    void Update()
    {
        if (z == 0)
        {
            Hand = GameObject.Find("Enemy_Hand");
            It.transform.SetParent(Hand.transform);
            It.transform.localScale = Vector3.one;
            It.transform.localPosition = new Vector3(It.transform.localPosition.x, It.transform.localPosition.y, It.transform.localPosition.z);

            It.transform.eulerAngles = new Vector3(0, 0, 0);
            z = 1;
        }

        id = thisCard[0].id;
        cardName = thisCard[0].cardName;
        cost = thisCard[0].cost;
        attack = thisCard[0].attack;
        defense = thisCard[0].defense;
        cardDescription = thisCard[0].cardDescription;
        thisSprite = thisCard[0].thisImage;
        drawXcards = thisCard[0].drawXcards;
        addXmaxMana = thisCard[0].addXmaxMana;

        returnXcards = thisCard[0].returnXcards;

        nameText.text = "" + cardName;
        costText.text = "" + cost;

        // actualpower = defense - hurted;
        actualpower = Mathf.Max(0, defense - hurted);

        atkText.text = "" + attack;
        defText.text = actualpower.ToString() + "/" + defense.ToString();
        if (healthBarImage != null && defense > 0)
        {
            float percent = (float)actualpower / (float)defense;
            healthBarImage.fillAmount = percent;
        }

        descriptionText.text = "" + cardDescription;
        thatImage.sprite = thisSprite;

        healXpower = thisCard[0].healXpower;

        if (this.tag == "Clones")
        {
            thisCard[0] = AI.staticEnemyDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck -= 1;
            AI.deckSize -= 1;
            this.tag = "Untagged";
        }

        if (hurted >= defense && thisCardCanBeDestroyed == true)
        {
            StartCoroutine(MoveToGraveyardAfterDelay());
            thisCardCanBeDestroyed = false;
        }

        if (this.transform.parent == Hand.transform)
        {
            cardBack.SetActive(true);
        }

        foreach (GameObject zone in AiZones)
        {
            if (this.transform.parent == zone.transform)
            {
                cardBack.SetActive(false);
                // KHÔNG reset summoning sickness ngay khi đặt vào zone
                // Bài vừa triệu hồi phải đợi đến lượt tiếp theo mới tấn công được
                Debug.Log($"AICard {cardName}: Placed in zone, summoningSickness remains: {summoningSickness}");
                break;
            }
        }

        // AI có thể tấn công khi:
        // 1. Không phải lượt của player
        // 2. Bài đã được triệu hồi (không còn summoning sickness)
        // 3. Bài đang ở trên sân (không phải trong hand)
        if (TurnSystem.isYourTurn == false && summoningSickness == false && isSummoned)
        {
            canAttack = true;
            Debug.Log($"AICard {cardName}: canAttack = true (AI turn, no summoning sickness, summoned)");
        }
        else
        {
            canAttack = false;
            Debug.Log($"AICard {cardName}: canAttack = false (isYourTurn={TurnSystem.isYourTurn}, summoningSickness={summoningSickness}, isSummoned={isSummoned})");
        }
        
        // KHÔNG reset summoning sickness ở đây - để AI.cs xử lý
        // Bài vừa triệu hồi phải đợi đến lượt tiếp theo mới tấn công được
        if (TurnSystem.isYourTurn == false && isSummoned && summoningSickness)
        {
            Debug.Log($"AICard {cardName}: summoningSickness still true (just summoned, cannot attack yet)");
        }
        
        foreach (GameObject zone in battleZones)
        {
            if (this.transform.parent == zone.transform && isSummoned == false)
            {
                // Bắt đầu hiệu ứng summon
                StartSummonEffect(zone.transform);
                
                if (drawXcards > 0)
                {
                    DrawX = drawXcards;
                    isSummoned = true;
                    break;
                }

                if (id == 23)
                {
                    TurnSystem.maxEnemyMana += 2;
                    isSummoned = true;
                }

                if (healXpower > 0)
                {
                    EnemyHp.staticHp += healXpower;
                    isSummoned = true;
                }

                isSummoned = true;
            }
        }
        
        // Debug: Kiểm tra xem Update có được gọi không
        Debug.Log($"AICard {cardName}: Update method called, parent={this.transform.parent?.name}");
        
        // Xử lý màu sắc của thẻ - đặt ở cuối để không bị override
        Debug.Log($"AICard {cardName}: About to call UpdateAICardColor");
        UpdateAICardColor();
        Debug.Log($"AICard {cardName}: UpdateAICardColor completed");
    }
    
    // Xử lý màu sắc của AI card
    private void UpdateAICardColor()
    {
        Debug.Log($"AICard {cardName}: UpdateAICardColor method STARTED");
        
        if (thatImage == null) 
        {
            Debug.Log($"AICard {cardName}: thatImage is NULL, returning early");
            return;
        }
        
        Debug.Log($"AICard {cardName}: thatImage is NOT null, continuing...");
        
        // Debug log để xem điều gì đang xảy ra
        Debug.Log($"AICard {cardName}: parent={this.transform.parent?.name}, isSummoned={isSummoned}, cost={cost}, maxMana={TurnSystem.maxEnemyMana}");
                
        // Thẻ đã vào mộ - trở về màu gốc
        if (this.transform.parent == Graveyard.transform)
        {
            Debug.Log($"AICard {cardName}: Setting graveyard color - original");
            thatImage.color = originalCardColor;
            return;
        }
        
        // Thẻ đã được triệu hồi và đang ở trên sân
        if (isSummoned && this.transform.parent != Hand.transform)
        {
            Debug.Log($"AICard {cardName}: Setting summoned color - original");
            thatImage.color = originalCardColor;
            return;
        }
        
        // Thẻ trong hand - kiểm tra có thể triệu hồi không
        if (this.transform.parent == Hand.transform)
        {
            // TEMPORARY TEST: Force tất cả thẻ trong hand thành màu xám để test
            Debug.Log($"AICard {cardName}: TEMPORARY TEST - Forcing gray color for all hand cards");
            thatImage.color = Color.grey;
            
            // Force update nhiều lần để đảm bảo
            thatImage.SetAllDirty();
            
            // Kiểm tra xem màu có được áp dụng không
            Debug.Log($"AICard {cardName}: Color after setting = {thatImage.color}");
            
            // Force update thêm
            if (thatImage.canvas != null)
            {
                // Loại bỏ ForceUpdateCanvases để tránh lỗi compile
                // thatImage.canvas.ForceUpdateCanvases();
            }
            
            return;
            
            // Kiểm tra có đủ mana để triệu hồi không
            if (cost <= TurnSystem.maxEnemyMana)
            {
                // Có thể triệu hồi - màu gốc
                Debug.Log($"AICard {cardName}: Setting hand color - original (can summon)");
                thatImage.color = originalCardColor;
            }
            else
            {
                // Không thể triệu hồi - màu xám chuẩn
                Debug.Log($"AICard {cardName}: Setting hand color - gray (cannot summon)");
                thatImage.color = Color.grey;
            }
        }
        else
        {
            // Thẻ ở nơi khác - giữ màu gốc
            Debug.Log($"AICard {cardName}: Setting other location color - original");
            thatImage.color = originalCardColor;
        }
        
        // Force update để đảm bảo màu sắc được áp dụng
        thatImage.SetAllDirty();
        
        // Force update CanvasRenderer để đảm bảo màu sắc hiển thị
        CanvasRenderer canvasRenderer = thatImage.GetComponent<CanvasRenderer>();
        if (canvasRenderer != null)
        {
            canvasRenderer.SetColor(thatImage.color);
        }
        
        // Force update thêm một lần nữa
        thatImage.enabled = false;
        thatImage.enabled = true;
        
        // Debug: In ra màu sắc hiện tại để kiểm tra
        Debug.Log($"AICard {cardName}: Final color set to {thatImage.color}");
    }

    public void BeingTarget()
    {
        isTarget = true;
    }

    public void DontBeingTarget()
    {
        isTarget = false;
    }

    IEnumerator AfterVoidStart()
    {
        yield return new WaitForSeconds(0.5f);
        thisCardCanBeDestroyed = true;
    }

    public void OnClickAICard()
    {
        if (thisCard.Count > 0 && cardBack.activeSelf == false)
        {
            CardInfoDisplay.instance.ShowCardInfo(thisCard[0]);
        }
    }

    public void OnExitAICard()
    {
        CardInfoDisplay.instance.HideCardInfo();
    }

    IEnumerator MoveToGraveyardAfterDelay()
    {
        yield return new WaitForSeconds(2f);

        this.transform.SetParent(Graveyard.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(45, 0, 0);
        hurted = 0;
        damageReceived = 0;

        if (healthBarImage != null)
            HealthBar.SetActive(false);
    }
    
    /// <summary>
    /// Bắt đầu hiệu ứng summon với lá bài rơi từ trên trời
    /// </summary>
    /// <param name="targetZone">Zone đích để đặt lá bài</param>
    private void StartSummonEffect(Transform targetZone)
    {
        // Kiểm tra xem có component AISummonEffect không
        var summonEffect = GetComponent<AISummonEffect>();
        
        if (summonEffect != null)
        {
            Debug.Log($"[AICard] {cardName}: Starting summon effect to {targetZone.name}");
            
            // Sử dụng hiệu ứng summon mới
            summonEffect.StartSummonEffect(targetZone, () => {
                // Callback khi hiệu ứng hoàn thành
                Debug.Log($"[AICard] {cardName}: Summon effect completed");
                
                // Đảm bảo lá bài được đặt đúng vị trí
                transform.SetParent(targetZone);
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;
                transform.localScale = Vector3.one;
            });
        }
        else
        {
            Debug.LogWarning($"[AICard] {cardName}: AISummonEffect component not found, using default placement");
            // Fallback về cách cũ nếu không có hiệu ứng
            transform.SetParent(targetZone);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
    
    public bool IsInBattleZone()
    {
        foreach (GameObject zone in battleZones)
        {
            if (this.transform.parent == zone.transform)
                return true;
        }
        return false;
    }
}
