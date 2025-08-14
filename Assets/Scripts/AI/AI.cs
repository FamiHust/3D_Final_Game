using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public enum AIType
{
    ThuyTinh,
    SonTinh,
    YeuMa,
    LacDieu
}

public class AI : MonoBehaviour
{
    public static AIType currentLevel;

    [Header("Data")]
    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticEnemyDeck = new List<Card>();

    public List<Card> cardsInHand = new List<Card>();  
    public List<Card> cardInZone  = new List<Card>(); 

    [Header("Scene Refs")]
    public GameObject Hand;
    public GameObject[] Zones = new GameObject[8];
    public GameObject[] playerZones = new GameObject[8];
    public GameObject Graveyard;

    [Header("Deck Visual Stacks")]
    [SerializeField] private GameObject cardInDeck1;
    [SerializeField] private GameObject cardInDeck2;
    [SerializeField] private GameObject cardInDeck3;
    [SerializeField] private GameObject cardInDeck4;
    [SerializeField] private GameObject cardInDeck5;
    [SerializeField] private GameObject cardInDeck6;
    [SerializeField] private GameObject cardInDeck7;

    [Header("Prefabs / Misc")]
    public GameObject CardBack;
    public GameObject aiCardToHand;
    public GameObject[] Clones;

    [Header("States")]
    public static bool draw;
    public int currentMana;
    public bool drawPhase;
    public bool summonPhase;
    public bool attackPhase;
    public bool endPhase;
    private bool isWaitingSummon;

    [Header("Summon helpers")]
    public bool[] AiCanSummon;
    public int[] cardsID;
    public int summonThisID;
    public int summonID;

    public int howManyCards;
    public int howManyCards_2;
    public int howManyCards_3;

    public static bool AiEndPhase;
    public AIType aiType;

    [Header("Avatars & Lands")]
    public GameObject avtSonTinh;
    public GameObject avtThuyTinh;
    public GameObject avtYeuMa;
    public GameObject avtLacDieu;
    public GameObject Thuy_Tinh_Land;
    public GameObject Son_Tinh_Land;
    public GameObject Yeu_Ma_Land;
    public GameObject Lac_Dieu_Land;

    [Header("UI")]
    public TextMeshProUGUI opponentNameText;

    private const int MaxHandMirror = 40;
    private const int MaxZoneMirror = 40;
    private const int DeckSizeDefault = 40;

    public static int deckSize;
    private int z;

    void Awake()
    {
        cardsInHand = Enumerable.Repeat(CardDatabase.cardList[0], MaxHandMirror).ToList();
        cardInZone  = Enumerable.Repeat(CardDatabase.cardList[0], MaxZoneMirror).ToList();
        AiCanSummon = new bool[MaxHandMirror];
        cardsID     = new int[MaxHandMirror];

        deckSize = DeckSizeDefault;

        Shuffle(); 
    }

    void Start()
    {
        avtSonTinh.SetActive(false);
        avtThuyTinh.SetActive(false);
        avtYeuMa.SetActive(false);
        avtLacDieu.SetActive(false);
        Thuy_Tinh_Land.SetActive(false);
        Son_Tinh_Land.SetActive(false);
        Yeu_Ma_Land.SetActive(false);
        Lac_Dieu_Land.SetActive(false);

        StartCoroutine(WaitFiveSeconds());

        Hand = GameObject.Find("Enemy_Hand");
        Graveyard = GameObject.Find("Enemy_Graveyard");
        aiType = ChampionSelector.selectedChampion;

        for (int i = 0; i < 8; i++)
        {
            Zones[i] = (i == 0) ? GameObject.Find("Enemy_Zone") : GameObject.Find("Enemy_Zone" + i);
        }
        for (int i = 0; i < 8; i++)
        {
            playerZones[i] = (i == 0) ? GameObject.Find("Zone") : GameObject.Find("Zone" + i);
        }

        draw = true;

        deck.Clear();
        for (int i = 0; i < deckSize; i++)
        {
            int pick = 0;
            if (aiType == AIType.ThuyTinh)
            {
                Thuy_Tinh_Land?.SetActive(true);
                avtThuyTinh?.SetActive(true);
                if (opponentNameText) opponentNameText.text = "Thuy Tinh";
                pick = Random.Range(32, 53);
            }
            else if (aiType == AIType.SonTinh)
            {
                Son_Tinh_Land?.SetActive(true);
                avtSonTinh?.SetActive(true);
                if (opponentNameText) opponentNameText.text = "Son Tinh";
                pick = Random.Range(52, 92);
            }
            else if (aiType == AIType.YeuMa)
            {
                Yeu_Ma_Land?.SetActive(true);
                avtYeuMa?.SetActive(true);
                if (opponentNameText) opponentNameText.text = "Yeu Ma";
                pick = Random.Range(91, 119);
            }
            else if (aiType == AIType.LacDieu)
            {
                Lac_Dieu_Land?.SetActive(true);
                avtLacDieu?.SetActive(true);
                if (opponentNameText) opponentNameText.text = "Lac Dieu";
                pick = Random.Range(120, 135);
            }
            deck.Add(CardDatabase.cardList[pick]);
        }

        // Sau khi deck đã có dữ liệu thực thì shuffle
        DoShuffleDeckList();

        Instantiate(CardBack, transform.position, transform.rotation);
        StartCoroutine(ShuffleNow());
    }

    void Update()
    {
        staticEnemyDeck = deck;

        // Cập nhật hiển thị số chồng bài
        if (deckSize < 30 && cardInDeck1) cardInDeck1.SetActive(false);
        if (deckSize < 20 && cardInDeck2) cardInDeck2.SetActive(false);
        if (deckSize < 10 && cardInDeck3) cardInDeck3.SetActive(false);
        if (deckSize < 5  && cardInDeck4) cardInDeck4.SetActive(false);
        if (deckSize < 3  && cardInDeck5) cardInDeck5.SetActive(false);
        if (deckSize < 2  && cardInDeck6) cardInDeck6.SetActive(false);
        if (deckSize < 1  && cardInDeck7) cardInDeck7.SetActive(false);

        if (AICardToHand.DrawX > 0)
        {
            StartCoroutine(Draw(AICardToHand.DrawX));
            AICardToHand.DrawX = 0;
        }

        int handSize = Hand ? Hand.transform.childCount : 0;

        // Draw phase (đầu lượt AI)
        if (!TurnSystem.startTurn && !draw && !TurnSystem.isYourTurn)
        {
            if (handSize < 5)
            {
                StartCoroutine(Draw(1));
                draw = true;
            }
        }

        currentMana = TurnSystem.currentEnemyMana;

        {
            int j = 0;
            howManyCards = 0;
            foreach (Transform child in Hand.transform)
            {
                howManyCards++;
            }
            foreach (Transform child in Hand.transform)
            {
                var aiCard = child.GetComponent<AICardToHand>();
                cardsInHand[j] = (aiCard != null) ? aiCard.thisCard[0] : CardDatabase.cardList[0];
                j++;
            }
            for (int i = j; i < MaxHandMirror; i++)
            {
                cardsInHand[i] = CardDatabase.cardList[0];
            }
        }

        if (!TurnSystem.isYourTurn)
        {
            for (int i = 0; i < MaxHandMirror; i++)
            {
                AiCanSummon[i] = (cardsInHand[i].id != 0 && currentMana >= cardsInHand[i].cost);
            }
        }
        else
        {
            for (int i = 0; i < MaxHandMirror; i++) AiCanSummon[i] = false;
        }

        if (!TurnSystem.isYourTurn) drawPhase = true;

        if (drawPhase && !summonPhase && !attackPhase && !isWaitingSummon)
        {
            isWaitingSummon = true;
            StartCoroutine(WaitForSummonPhase());
        }

        if (TurnSystem.isYourTurn)
        {
            drawPhase = false;
            summonPhase = false;
            attackPhase = false;
            endPhase = false;
            return;
        }

        if (summonPhase)
        {
            DoSummonLoop();
            summonPhase = false;
            attackPhase = true;
        }

        // Mirror zone (không dùng để quyết định tấn công)
        {
            int l = 0;
            howManyCards_3 = 0;
            foreach (GameObject zone in Zones)
            {
                foreach (Transform child in zone.transform)
                {
                    howManyCards_3++;
                    var aiCard = child.GetComponent<AICardToHand>();
                    cardInZone[l] = (aiCard != null) ? aiCard.thisCard[0] : CardDatabase.cardList[0];
                    l++;
                }
            }
            for (int i = l; i < MaxZoneMirror; i++)
            {
                cardInZone[i] = CardDatabase.cardList[0];
            }
        }

        if (attackPhase && !endPhase)
        {
            DoAttackPhase();
            endPhase = true;
        }

        if (endPhase)
        {
            AiEndPhase = true;
        }
    }

    public void Shuffle()
    {
        Instantiate(CardBack, transform.position, transform.rotation);
        StartCoroutine(ShuffleNow());
    }

    private void DoShuffleDeckList()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            int r = Random.Range(i, deck.Count);
            var tmp = deck[i];
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }

    private void DoSummonLoop()
    {
        bool hasSummoned;
        int safety = 50;
        do
        {
            hasSummoned = false;
            int index = 0;
            summonID = 0;
            summonThisID = 0;

            // Lọc ID các lá có thể triệu hồi (ưu tiên ID lớn nhất)
            for (int i = 0; i < MaxHandMirror; i++)
            {
                if (AiCanSummon[i] && cardsInHand[i].cost <= currentMana)
                {
                    cardsID[index] = cardsInHand[i].id;
                    index++;
                }
            }

            // Chọn ID max
            for (int i = 0; i < index; i++)
            {
                if (cardsID[i] > summonID) summonID = cardsID[i];
            }
            summonThisID = summonID;

            if (summonThisID == 0) break;

            // Tìm lá trong tay có id = summonThisID và đặt vào ô trống
            Transform targetChild = null;
            foreach (Transform child in Hand.transform)
            {
                var aiCard = child.GetComponent<AICardToHand>();
                if (aiCard != null && aiCard.id == summonThisID && CardDatabase.cardList[summonThisID].cost <= currentMana)
                {
                    targetChild = child;
                    break;
                }
            }

            if (targetChild != null)
            {
                GameObject freeZone = Zones.FirstOrDefault(z => z.transform.childCount == 0);
                if (freeZone != null)
                {
                    targetChild.SetParent(freeZone.transform);
                    targetChild.localPosition = Vector3.zero;
                    targetChild.localRotation = Quaternion.identity;
                    targetChild.localScale = Vector3.zero;
                    targetChild.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

                    int cost = CardDatabase.cardList[summonThisID].cost;
                    if (TurnSystem.currentEnemyMana >= cost)
                    {
                        TurnSystem.currentEnemyMana -= cost;
                        SoundManager.PlaySound(SoundType.Summon);
                        hasSummoned = true;
                    }
                }
            }

            safety--;
            if (safety <= 0) break;
        } while (hasSummoned);
    }

    private void DoAttackPhase()
    {
        var enemyUnits = Zones
            .SelectMany(z => z.transform.Cast<Transform>())
            .Select(t => new { tr = t, ai = t.GetComponent<AICardToHand>() })
            .Where(x => x.ai != null && x.ai.canAttack)
            .ToList();

        var playerUnits = playerZones
            .SelectMany(z => z.transform.Cast<Transform>())
            .Select(t => new { tr = t, pc = t.GetComponent<ThisCard>() })
            .Where(x => x.pc != null)
            .ToList();

        if (playerUnits.Count > 0)
        {
            // Đánh vào bài (ưu tiên “mục tiêu đầu tiên” cho đơn giản)
            foreach (var atk in enemyUnits)
            {
                if (playerUnits.Count == 0) break;

                var target = playerUnits[0];
                var attackerCard = atk.ai.thisCard[0];
                var targetCard   = target.pc.thisCard[0];

                ApplyAIDamageToPlayerCard(target.pc, attackerCard, atk.ai, targetCard);

                atk.ai.canAttack = false;
                playerUnits.RemoveAt(0);
            }
        }
        else
        {
            // Không còn bài của Player trên sân thì đánh thẳng vào HP
            foreach (var atk in enemyUnits)
            {
                var atkCard = atk.ai.thisCard[0];
                PlayerHp.staticHp -= atkCard.attack;
                atk.ai.canAttack = false;
                CameraShake.instance.Shake();
                SoundManager.PlaySound(SoundType.Attack);
            }
        }
    }

    public void AIStartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.PlaySound(SoundType.Draw);
            Instantiate(aiCardToHand, transform.position, transform.rotation, Hand.transform);
        }
    }

    IEnumerator ShuffleNow()
    {
        yield return new WaitForSeconds(0.5f);
        Clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject Clone in Clones)
        {
            Destroy(Clone);
        }
    }

    IEnumerator Draw(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.PlaySound(SoundType.Draw);
            Instantiate(aiCardToHand, transform.position, transform.rotation, Hand.transform);
            if (deckSize > 0) deckSize--;
        }
    }

    IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(5f);
    }

    IEnumerator WaitForSummonPhase()
    {
        yield return new WaitForSeconds(Random.Range(5f, 10f));
        summonPhase = true;
        isWaitingSummon = false;
    }

    private void ApplyAIDamageToPlayerCard(ThisCard targetCardScript, Card attacker, AICardToHand attackingAICard, Card targetCard)
    {
        // Gây sát thương cho bài của Player
        targetCardScript.hurted += attacker.attack;

        if (attacker.attack < targetCard.defense)
        {
            attackingAICard.hurted += (targetCard.defense - attacker.attack);
        }

        if (attacker.attack == targetCard.defense)
        {
            attackingAICard.hurted += targetCard.defense;
        }

        AIEffect effect = targetCardScript.GetComponentInChildren<AIEffect>();
        if (effect != null)
        {
            effect.PlayHurtAnimation();
        }

        SoundManager.PlaySound(SoundType.Attack);
        CameraShake.instance.Recoil();
    }
}
