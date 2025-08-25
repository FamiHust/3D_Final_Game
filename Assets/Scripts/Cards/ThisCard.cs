using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ThisCard : MonoBehaviour
{
    public List<Card> thisCard = new List<Card>();
    public int thisID;
    public int id;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;
    public string cardName;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI defText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private Sprite thisSprite;
    [SerializeField] private Image thatImage;
    [SerializeField] private Image healthBarImage;
    
    private Color originalCardColor;


    public bool cardBack;
    public static bool staticCardBack;

    public int numberOfCardsInDeck;
    public bool canBeSummon;
    public bool summoned;

    public static int drawX;
    public int drawXcards;
    public int addXmaxMana;

    public GameObject Hand;
    public GameObject attackBorder;
    public GameObject Target;
    public GameObject Enemy;
    public GameObject summonBorder;
    public GameObject Graveyard;
    public GameObject HealEffect;
    public GameObject SummonEffect;
    [SerializeField] private GameObject HealthBar;

    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;
    public static bool staticTargeting;
    public static bool staticTargetingEnemy;
    public bool targeting;
    public bool targetingEnemy;
    public bool onlyThisCardAttack;
    public bool canBeDestroyed;
    public bool beInGraveyard;
    public bool useReturn;
    public static bool UcanReturn;
    public bool canHeal;
    public bool spell;
    public bool dealDamage;
    public bool stopDealDamage;

    public int hurted;
    public int actualpower;
    public int returnXcards;
    public int healXpower;
    public int damageDealBySpell;
    public int damageReceived; // Sát thương nhận được khi bị tấn công

    public GameObject[] battleZones = new GameObject[8];
    public GameObject[] EnemyZones = new GameObject[8];
    
    void Start()
    {
        thisCard[0] = CardDatabase.cardList[thisID];
        numberOfCardsInDeck = PlayerDeck.deckSize;

        canBeSummon = false;
        summoned = false;
        drawXcards = 0;
        canAttack = false;
        summoningSickness = true;
        canHeal = true;
        damageReceived = 0;

        Enemy = GameObject.Find("Health_Bar");
        Debug.Log($"[ThisCard] Enemy object found: {(Enemy != null ? Enemy.name : "null")}");

        for (int i = 0; i < 8; i++)
        {
            EnemyZones[i] = GameObject.Find(i == 0 ? "Enemy_Zone" : "Enemy_Zone" + i);
            battleZones[i] = GameObject.Find(i == 0 ? "Zone" : "Zone" + i);
        }

        // Lưu màu gốc của thẻ
        if (thatImage != null)
        {
            originalCardColor = thatImage.color;
        }
    }

    void Update()
    {
        Hand = GameObject.Find("My_Hands");

        if (this.transform.parent == Hand.transform.parent)
        {
            cardBack = false;
        }

        Card card = thisCard[0];
        id = card.id;
        cardName = card.cardName;
        cost = card.cost;
        attack = card.attack;
        defense = card.defense;
        cardDescription = card.cardDescription;
        thisSprite = card.thisImage;
        drawXcards = card.drawXcards;
        addXmaxMana = card.addXmaxMana;
        returnXcards = card.returnXcards;
        healXpower = card.healXpower;
        spell = card.spell;
        damageDealBySpell = card.damageDealBySpell;

        nameText.text = cardName;
        costText.text = cost.ToString();
        
        // actualpower = defense - hurted;
        actualpower = Mathf.Max(0, defense - hurted);

        atkText.text = attack.ToString();
        defText.text = actualpower.ToString() + "/" + defense.ToString();

        if (healthBarImage != null && defense > 0)
        {
            float percent = (float)actualpower / (float)defense;
            healthBarImage.fillAmount = percent;
        }

        if (HealthBar != null)
        {
            HealthBar.SetActive(!spell && !beInGraveyard);
        }

        descriptionText.text = cardDescription;
        thatImage.sprite = thisSprite;

        staticCardBack = cardBack;

        if (this.tag == "Clones")
        {
            thisCard[0] = PlayerDeck.staticDeck[numberOfCardsInDeck - 1];
            numberOfCardsInDeck--;
            PlayerDeck.deckSize--;
            cardBack = false;
            this.tag = "Untagged";
        }

        canBeSummon = TurnSystem.currentMana >= cost && !summoned && !beInGraveyard && TurnSystem.isYourTurn;

        gameObject.GetComponent<Draggable>().enabled = canBeSummon;

        // Thay đổi màu sắc dựa trên khả năng triệu hồi
        UpdateCardColor();

        if (!summoned)
        {
            foreach (GameObject zone in battleZones)
            {
                if (this.transform.parent == zone.transform)
                {
                    var zoneElement = zone.GetComponent<ZoneElement>();
                    if (zoneElement != null && zoneElement.elementType == thisCard[0].elementType)
                    {
                        Summon();
                    }
                    else
                    {
                        this.transform.SetParent(Hand.transform);
                        transform.localPosition = Vector3.zero;
                        transform.localRotation = Quaternion.identity;
                        transform.localScale = Vector3.one;
                    }
                    break;
                }
            }
        }

        if (!TurnSystem.isYourTurn && summoned)
        {
            summoningSickness = false;
            cantAttack = false;
        }

        canAttack = TurnSystem.isYourTurn && !summoningSickness && !cantAttack;

        targeting = staticTargeting;
        targetingEnemy = staticTargetingEnemy;

        Target = targetingEnemy ? Enemy : null;
        Debug.Log($"[ThisCard] Target set - targetingEnemy: {targetingEnemy}, Target: {(Target != null ? Target.name : "null")}, Enemy: {(Enemy != null ? Enemy.name : "null")}");

        Debug.Log($"[ThisCard] Attack condition check - targeting: {targeting}, onlyThisCardAttack: {onlyThisCardAttack}");
        if (targeting && onlyThisCardAttack)
        {
            Debug.Log("[ThisCard] Attack condition met - calling Attack()");
            Attack();
        }

        if (canBeSummon)
        {
            summonBorder.SetActive(true);
        }

        else if (beInGraveyard && UcanReturn)
        {
            summonBorder.SetActive(true);
        }
        else
        {
            summonBorder.SetActive(false);
        }

        if (actualpower <= 0 && !spell)
        {
            Destroy();
        }

        if (returnXcards > 0 && summoned && !useReturn && TurnSystem.isYourTurn)
        {
            Return(returnXcards);
            useReturn = true;
        }

        if (!TurnSystem.isYourTurn)
        {
            UcanReturn = false;
        }

        if (canHeal && summoned && spell && healXpower > 0)
        {
            Heal();
            canHeal = false;
        }

        if (damageDealBySpell > 0)
        {
            dealDamage = true;
        }

        HandleAttackBorderDisplay();

        if (dealDamage && IsInAnyBattleZone())
        {
            if (Input.GetMouseButtonDown(0))
            {
                dealxDamage(damageDealBySpell);
            }
        }

        if (stopDealDamage)
        {
            attackBorder.SetActive(false);
            dealDamage = false;
            
            // Tắt effect chỉ mục tiêu khi spell bị phá hủy hoặc hết hiệu lực
            if (spell)
            {
                var enemyBoardWatcher = FindObjectOfType<EnemyBoardWatcher>();
                if (enemyBoardWatcher != null)
                {
                    enemyBoardWatcher.HideTargetEffectDirectly();
                }
            }
        }

        if (IsInAnyBattleZone() && spell && !dealDamage)
        {
            StartCoroutine(Wait());
        }

        bool isInBattleZone = false;

        foreach (GameObject zone in battleZones)
        {
            if (this.transform.parent == zone.transform)
            {
                isInBattleZone = true;
                break;
            }
        }
        // Chỉ bật HealthBar nếu đang ở BattleZone, không phải spell, không ở mộ bài
        HealthBar.SetActive(isInBattleZone && !spell && !beInGraveyard);
    }

    void HandleAttackBorderDisplay()
    {
        bool shouldShowAttackBorder = false;

        if (canAttack && !beInGraveyard)
            shouldShowAttackBorder = true;

        if (dealDamage && IsInAnyBattleZone())
            shouldShowAttackBorder = true;

        if (stopDealDamage || (spell && dealDamage == false && IsInAnyBattleZone()))
        {
            shouldShowAttackBorder = false;
            
            // Tắt effect chỉ mục tiêu khi spell bị phá hủy hoặc hết hiệu lực
            if (spell && dealDamage == false)
            {
                var enemyBoardWatcher = FindObjectOfType<EnemyBoardWatcher>();
                if (enemyBoardWatcher != null)
                {
                    enemyBoardWatcher.HideTargetEffectDirectly();
                }
            }
        }

        attackBorder.SetActive(shouldShowAttackBorder);
    }

    bool IsInAnyBattleZone()
    {
        foreach (GameObject zone in battleZones)
        {
            if (this.transform.parent == zone.transform)
                return true;
        }
        return false;
    }

    public void Summon()
    {
        TurnSystem.currentMana -= cost;
        summoned = true;
        MaxMana(addXmaxMana);
        drawX = drawXcards;
        SoundManager.PlaySound(SoundType.Summon);

        SummonEffect.SetActive(true);
        HealthBar.SetActive(true);
    }

    public void MaxMana(int x)
    {
        TurnSystem.maxMana += x;
    }

    public void Attack()
    {   
        Debug.Log($"[ThisCard] Attack() called - canAttack: {canAttack}, summoned: {summoned}");
        
        if (canAttack && summoned)
        {
            Debug.Log("[ThisCard] Card can attack and is summoned");
            bool enemyHasCards = false;

            foreach (GameObject zone in EnemyZones)
            {
                if (zone.transform.childCount > 0)
                {
                    enemyHasCards = true;
                    break;
                }
            }
            
            Debug.Log($"[ThisCard] Enemy has cards: {enemyHasCards}");

            Debug.Log($"[ThisCard] Target check - Target: {(Target != null ? Target.name : "null")}, Enemy: {(Enemy != null ? Enemy.name : "null")}");
            if (Target == Enemy)
            {
                Debug.Log("[ThisCard] Target is Enemy - proceeding with direct HP attack");
                if (!spell && enemyHasCards)
                {
                    Debug.Log("[ThisCard] Not a spell and enemy has cards - returning early");
                    return;
                }

                // Vẽ line tấn công khi đánh trực tiếp vào máu của AI
                Debug.Log("[ThisCard] Attempting to draw attack line to AI HP...");
                if (SimpleParticleManager.Instance != null)
                {
                    Debug.Log("[ThisCard] SimpleParticleManager.Instance found");
                    var fromRt = transform as RectTransform;
                    if (fromRt != null)
                    {
                        Debug.Log("[ThisCard] fromRt is valid");
                        var enemyHpComponent = Enemy.GetComponent<EnemyHp>();
                        Debug.Log($"[ThisCard] EnemyHp component: {(enemyHpComponent != null ? "found" : "null")}");
                        
                        if (enemyHpComponent != null && enemyHpComponent.EnemyModel != null && Camera.main != null)
                        {
                            Debug.Log("[ThisCard] Using Enemy Model for attack line");
                            // Sử dụng world position của enemy model để vẽ line
                            var enemyModelWorldPos = enemyHpComponent.EnemyModel.position;
                            var enemyModelScreenPos = Camera.main.WorldToScreenPoint(enemyModelWorldPos);
                            var enemyModelCanvasPos = Vector2.zero;
                            GameObject tempTarget = null;
                            RectTransform tempTargetRt = null;
                            
                            if (fromRt.parent != null)
                            {
                                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                                    fromRt.parent as RectTransform, 
                                    enemyModelScreenPos, 
                                    Camera.main, 
                                    out enemyModelCanvasPos
                                );
                                
                                Debug.Log($"[ThisCard] Enemy model canvas position: {enemyModelCanvasPos}");
                                
                                // Tạo một GameObject tạm thời để làm target cho attack line
                                tempTarget = new GameObject("TempEnemyModelTarget");
                                tempTargetRt = tempTarget.AddComponent<RectTransform>();
                                tempTargetRt.SetParent(fromRt.parent);
                                tempTargetRt.localPosition = enemyModelCanvasPos;
                                tempTargetRt.sizeDelta = Vector2.one;
                                
                                if (tempTargetRt != null)
                                {
                                    Debug.Log("[ThisCard] Drawing attack line to enemy model");
                                    SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, tempTargetRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                                    
                                    // Xóa GameObject tạm thời sau khi line effect hoàn thành
                                    Destroy(tempTarget, 0.7f);
                                }
                            }
                            else
                            {
                                Debug.LogWarning("[ThisCard] fromRt.parent is null!");
                            }
                        }
                        else
                        {
                            Debug.Log("[ThisCard] Using HP bar fallback for attack line");
                            // Fallback: vẽ line đến HP bar nếu không có model
                            var enemyHpRt = Enemy.GetComponent<RectTransform>();
                            if (enemyHpRt != null)
                            {
                                Debug.Log("[ThisCard] Drawing attack line to HP bar");
                                SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, enemyHpRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                            }
                            else
                            {
                                Debug.LogError("[ThisCard] Enemy HP RectTransform is null!");
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("[ThisCard] fromRt is null!");
                    }
                }
                else
                {
                    Debug.LogError("[ThisCard] SimpleParticleManager.Instance is null!");
                }

                Debug.Log($"[ThisCard] Direct attack to AI HP: {attack} damage, current AI HP: {EnemyHp.staticHp}");
                EnemyHp.staticHp -= attack;
                Debug.Log($"[ThisCard] AI HP after attack: {EnemyHp.staticHp}");
                StartCoroutine(DelayAttackEffects(0.5f));

                targeting = false;
                cantAttack = true;
            }
            else
            {
                Debug.Log("[ThisCard] Target is not Enemy - looking for AI cards to attack");
                foreach (GameObject zone in EnemyZones)
                {
                    if (zone.transform.childCount > 0)
                    {
                        var aiCard = zone.transform.GetChild(0).GetComponent<AICardToHand>();
                        Debug.Log($"[ThisCard] Found AI card in zone: {(aiCard != null ? aiCard.name : "null")}, isTarget: {(aiCard != null ? aiCard.isTarget.ToString() : "N/A")}");
                        if (aiCard != null && aiCard.isTarget)
                        {
                            aiCard.hurted += attack;
                            
                            // Ghi nhận sát thương nhận được
                            aiCard.damageReceived = attack;
                            
                            // Chỉ nhận phản damage khi attack < defense của địch
                            // Lượng damage bị phản = defense của địch - attack của mình
                            if (attack < aiCard.defense)
                            {
                                hurted += (aiCard.defense - attack);
                            }

                            if (attack == aiCard.defense)
                            {
                                hurted += aiCard.defense;
                            }

                            // Vẽ line tấn công giữa 2 lá bài (UI trong canvas)
                            if (SimpleParticleManager.Instance != null)
                            {
                                var fromRt = transform as RectTransform;
                                var toRt = aiCard.transform as RectTransform;
                                if (fromRt != null && toRt != null)
                                {
                                    SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, toRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                                }
                            }

                            AIEffect effect = aiCard.GetComponentInChildren<AIEffect>();

                            if (aiCard.IsInBattleZone())
                            {
                                effect.PlayHurtAnimation();
                                // Hiển thị popup sát thương
                                if (DamagePopupManager.Instance != null)
                                {
                                    DamagePopupManager.Instance.ShowEnemyDamagePopup(aiCard.transform.position, attack);
                                }
                            }

                            cantAttack = true;
                            SoundManager.PlaySound(SoundType.Attack);
                            CameraShake.instance.Shake();

                            break;
                        }
                    }
                }
            }
        }
    }

    public void UntargetEnemy() => staticTargetingEnemy = false;
    public void TargetEnemy() => staticTargetingEnemy = true;
    public void StartAttack() => staticTargeting = true;
    public void StopAttack() => staticTargeting = false;
    public void OneCardAttack() => onlyThisCardAttack = true;
    public void OneCardStopAttack() => onlyThisCardAttack = false;

    public void Destroy()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(2f);
        Graveyard = GameObject.Find("My_Graveyard");
        canBeDestroyed = true;

        if (canBeDestroyed)
        {
            this.transform.SetParent(Graveyard.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(45, 0, 0);

            canBeDestroyed = false;
            summoned = false;
            beInGraveyard = true;
            hurted = 0;
            damageReceived = 0;
            if (spell)
            {
                gameObject.SetActive(false);
            }

            HealthBar.SetActive(false);
        }
    }

    public void Return(int x)
    {
        for (int i = 0; i < x; i++)
        {
            ReturnCard();
        }
    }

    public void ReturnCard()
    {
        UcanReturn = true;
    }

    public void ReturnThis()
    {
        if (beInGraveyard && UcanReturn)
        {
            this.transform.SetParent(Hand.transform);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            UcanReturn = false;
            beInGraveyard = false;
            summoningSickness = true;
            damageReceived = 0;

            HealthBar.SetActive(true);
        }
    }

    public void Heal()
    {
        PlayerHp.staticHp += healXpower;
        HealEffect.SetActive(true);
        SoundManager.PlaySound(SoundType.Heal);
    }

    public void dealxDamage(int x)
    {        
        if (Target == Enemy && !stopDealDamage)
        {
            // Vẽ line tấn công khi dùng spell đánh trực tiếp vào máu của AI
            if (SimpleParticleManager.Instance != null)
            {
                var fromRt = transform as RectTransform;
                var enemyHpComponent = Enemy.GetComponent<EnemyHp>();
                if (enemyHpComponent != null && enemyHpComponent.EnemyModel != null)
                {
                    // Sử dụng world position của enemy model để vẽ line
                    var enemyModelWorldPos = enemyHpComponent.EnemyModel.position;
                    var enemyModelScreenPos = Camera.main.WorldToScreenPoint(enemyModelWorldPos);
                    var enemyModelCanvasPos = Vector2.zero;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        fromRt.parent as RectTransform, 
                        enemyModelScreenPos, 
                        Camera.main, 
                        out enemyModelCanvasPos
                    );
                    
                    // Tạo một GameObject tạm thời để làm target cho attack line
                    var tempTarget = new GameObject("TempEnemyModelTarget");
                    var tempTargetRt = tempTarget.AddComponent<RectTransform>();
                    tempTargetRt.SetParent(fromRt.parent);
                    tempTargetRt.localPosition = enemyModelCanvasPos;
                    tempTargetRt.sizeDelta = Vector2.one;
                    
                                            SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, tempTargetRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                    
                    // Xóa GameObject tạm thời sau khi line effect hoàn thành
                    Destroy(tempTarget, 0.7f);
                }
                                    else
                    {
                        // Fallback: vẽ line đến HP bar nếu không có model
                        var fromRtFallback = transform as RectTransform;
                        var enemyHpRt = Enemy.GetComponent<RectTransform>();
                        if (fromRtFallback != null && enemyHpRt != null)
                        {
                            SimpleParticleManager.Instance.ShowAttackDashedLine(fromRtFallback, enemyHpRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                        }
                    }
            }

            EnemyHp.staticHp -= damageDealBySpell;
            StartCoroutine(DelayAttackEffects(0.5f));

            stopDealDamage = true;
        }
        else
        {
            foreach (GameObject zone in EnemyZones)
            {
                if (zone.transform.childCount > 0)
                {
                    var aiCard = zone.transform.GetChild(0).GetComponent<AICardToHand>();
                    if (aiCard != null && aiCard.isTarget)
                    {
                        // Vẽ line tấn công giữa 2 lá bài (UI trong canvas)
                        if (SimpleParticleManager.Instance != null)
                        {
                            var fromRt = transform as RectTransform;
                            var toRt = aiCard.transform as RectTransform;
                            if (fromRt != null && toRt != null)
                            {
                                SimpleParticleManager.Instance.ShowAttackDashedLine(fromRt, toRt, 6f, 26f, 16f, 36f, 0.7f, Color.white, 180f);
                            }
                        }

                        aiCard.hurted += damageDealBySpell;

                        AIEffect effect = aiCard.GetComponentInChildren<AIEffect>();

                        effect.PlayHurtAnimation();

                        SoundManager.PlaySound(SoundType.Attack);
                        CameraShake.instance.Shake();

                        stopDealDamage = true;  
                        break;
                    }
                }
            }
        }
    }

    public void OnCardClick()
    {
        if (thisCard.Count > 0 && !cardBack)
        {
            CardInfoDisplay.instance.ShowCardInfo(thisCard[0]);
        }
    }

    public void OnCardExit()
    {
        CardInfoDisplay.instance.HideCardInfo();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy();
    }

    private void UpdateCardColor()
    {
        if (thatImage == null) return;

        // Thẻ đã vào mộ - trở về màu gốc
        if (beInGraveyard)
        {
            thatImage.color = originalCardColor;
            return;
        }

        // Thẻ đã được triệu hồi và đang ở trên sân luôn sáng
        if (summoned && IsInAnyBattleZone())
        {
            thatImage.color = originalCardColor;
            return;
        }

        if (!canBeSummon)
        {
            // Màu xám cho thẻ không thể triệu hồi (chỉ áp dụng cho thẻ trong hand)
            thatImage.color = new Color(0.6f, 0.6f, 1f);
        }
        else
        {
            // Khôi phục màu gốc cho thẻ có thể triệu hồi
            thatImage.color = originalCardColor;
        }
    }
    
    // Delay sound attack và camera shake khi tấn công trực tiếp vào máu
    private IEnumerator DelayAttackEffects(float delaySeconds)
    {
        yield return new WaitForSeconds(delaySeconds);
        SoundManager.PlaySound(SoundType.Attack);
        if (CameraShake.instance != null)
        {
            CameraShake.instance.Shake();
        }
    }
}
