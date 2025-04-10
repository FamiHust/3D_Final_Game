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
    public string cardName;
    public int cost;
    public int attack;
    public int defense;
    public string cardDescription;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;
    public TextMeshProUGUI descriptionText;

    public Sprite thisSprite;
    public Image thatImage;
    public GameObject Hand;

    public bool cardBack;
    public static bool staticCardBack;

    public int numberOfCardsInDeck;
    public bool canBeSummon;
    public bool summoned;

    public static int drawX;
    public int drawXcards;
    public int addXmaxMana;

    public GameObject attackBorder;
    public GameObject Target;
    public GameObject Enemy;
    public bool summoningSickness;
    public bool cantAttack;
    public bool canAttack;
    public static bool staticTargeting;
    public static bool staticTargetingEnemy;

    public bool targeting;
    public bool targetingEnemy;
    public bool onlyThisCardAttack;

    public GameObject summonBorder;
    public GameObject Graveyard;

    public bool canBeDestroyed;
    public bool beInGraveyard;

    public int hurted;
    public int actualpower;
    public int returnXcards;
    public bool useReturn;

    public static bool UcanReturn;

    public int healXpower;
    public bool canHeal;

    public GameObject[] battleZones = new GameObject[8];
    public GameObject[] EnemyZones = new GameObject[8];
    public AICardToHand aiCardToHand;

    public bool spell;
    public int damageDealBySpell;
    public bool dealDamage;
    public bool stopDealDamage;

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

        Enemy = GameObject.Find("Health_Bar");

        for (int i = 0; i < 8; i++)
        {
            EnemyZones[i] = GameObject.Find(i == 0 ? "Enemy_Zone" : "Enemy_Zone" + i);
            battleZones[i] = GameObject.Find(i == 0 ? "Zone" : "Zone" + i);
        }
    }

    void Update()
    {
        Hand = GameObject.Find("My_Hands");

        if (this.transform.parent == Hand.transform.parent)
        {
            cardBack = false;
        }

        // Gán dữ liệu từ card database
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

        // Update UI
        nameText.text = cardName;
        costText.text = cost.ToString();
        actualpower = defense - hurted;
        atkText.text = attack.ToString();
        defText.text = actualpower.ToString();
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

        if (!summoned)
        {
            foreach (GameObject zone in battleZones)
            {
                if (this.transform.parent == zone.transform)
                {
                    Summon();
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

        if (targeting && onlyThisCardAttack)
        {
            Attack();
        }

        // summonBorder.SetActive((canBeSummon || UcanReturn) && beInGraveyard);
        // Khi bài chưa ở mộ và đủ điều kiện triệu hồi bình thường
        if (canBeSummon)
        {
            summonBorder.SetActive(true);
        }
        // Khi bài đang ở mộ và được hồi sinh
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

        if (canHeal && summoned)
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
            dealxDamage(damageDealBySpell);
        }

        if (stopDealDamage)
        {
            attackBorder.SetActive(false);
            dealDamage = false;
        }

        if (IsInAnyBattleZone() && spell && !dealDamage)
        {
            StartCoroutine(Wait());
        }
    }

    void HandleAttackBorderDisplay()
    {
        bool shouldShowAttackBorder = false;

        if (canAttack && !beInGraveyard)
            shouldShowAttackBorder = true;

        if (dealDamage && IsInAnyBattleZone())
            shouldShowAttackBorder = true;

        if (stopDealDamage || (spell && dealDamage == false && IsInAnyBattleZone()))
            shouldShowAttackBorder = false;

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
    }

    public void MaxMana(int x)
    {
        TurnSystem.maxMana += x;
    }

    public void Attack()
    {   
        if (canAttack && summoned)
        {
            bool enemyHasCards = false;

            // Kiểm tra enemy còn bài không
            foreach (GameObject zone in EnemyZones)
            {
                if (zone.transform.childCount > 0)
                {
                    enemyHasCards = true;
                    break;
                }
            }

            if (Target == Enemy)
            {
                if (!spell && enemyHasCards)
                {
                    return;
                }

                EnemyHp.staticHp -= attack;
                targeting = false;
                cantAttack = true;
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
                            aiCard.hurted = attack;
                            hurted = aiCard.defense;
                            cantAttack = true;
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
        }
    }

    public void Heal()
    {
        PlayerHp.staticHp += healXpower;
    }

    public void dealxDamage(int x)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Target == Enemy && !stopDealDamage)
            {
                EnemyHp.staticHp -= damageDealBySpell;
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
                            aiCard.hurted += damageDealBySpell;
                            stopDealDamage = true;
                            break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy();
    }
}
