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

    public List<Card> deck = new List<Card>();
    public List<Card> container = new List<Card>();
    public static List<Card> staticEnemyDeck = new List<Card>();
    public List<Card> cardsInHand = new List<Card>();
    public List<Card> cardInZone = new List<Card>();

    public GameObject Hand;
    public GameObject[] Zones = new GameObject[8];
    public GameObject[] playerZones = new GameObject[8];
    public GameObject Graveyard;

    [SerializeField] private int z;
    public static int deckSize;

    [SerializeField] private GameObject cardInDeck1;
    [SerializeField] private GameObject cardInDeck2;
    [SerializeField] private GameObject cardInDeck3;
    [SerializeField] private GameObject cardInDeck4;
    [SerializeField] private GameObject cardInDeck5;
    [SerializeField] private GameObject cardInDeck6;
    [SerializeField] private GameObject cardInDeck7;

    public GameObject CardBack;
    public GameObject aiCardToHand;
    public GameObject[] Clones;
    public static bool draw;

    public int currentMana;
    public bool[] AiCanSummon;
    public bool drawPhase;
    public bool summonPhase;
    public bool attackPhase;
    public bool endPhase;
    public int[] cardsID;
    public int summonThisID;
    public int summonID;

    AICardToHand AiCardToHand;
    public int sumonID;
    public int howManyCards;
    public int howManyCards_2;
    public int howManyCards_3;

    public bool[] canAttack;
    public static bool AiEndPhase;
    public AIType aiType;

    public GameObject avtSonTinh;
    public GameObject avtThuyTinh;
    public GameObject avtYeuMa;
    public GameObject avtLacDieu;
    public GameObject Thuy_Tinh_Land;
    public GameObject Son_Tinh_Land;
    public GameObject Yeu_Ma_Land;
    public GameObject Lac_Dieu_Land;

    public TextMeshProUGUI opponentNameText;

    void Awake()
    {
        Shuffle();
    }

    // Start is called before the first frame update
    void Start()
    {
        avtSonTinh.SetActive(false);
        avtThuyTinh.SetActive(false);
        avtYeuMa.SetActive(false);

        StartCoroutine(WaitFiveSeconds());
        // AIStartGame();

        Hand = GameObject.Find("Enemy_Hand");
        Graveyard = GameObject.Find("Enemy_Graveyard");
        aiType = ChampionSelector.selectedChampion;

        for (int i = 0; i < 8; i++)
        {
            if (i == 0) Zones[i] = GameObject.Find("Enemy_Zone");
            else Zones[i] = GameObject.Find("Enemy_Zone" + i);
        }

        for (int i = 0; i < 8; i++)
        {
            if (i == 0) playerZones[i] = GameObject.Find("Zone");
            else playerZones[i] = GameObject.Find("Zone" + i);
        }

        z = 0;
        deckSize = 40;
        draw = true;

        for (int i = 0; i < deckSize; i++)
        {
            if (aiType == AIType.ThuyTinh)
            {
                Thuy_Tinh_Land.SetActive(true);
                avtThuyTinh.SetActive(true);
                opponentNameText.text = "Thuy Tinh";
                z = Random.Range(32, 53);
            }
            else if (aiType == AIType.SonTinh)
            {
                Son_Tinh_Land.SetActive(true);
                avtSonTinh.SetActive(true);
                opponentNameText.text = "Son Tinh";
                z = Random.Range(52, 92);
            }
            else if (aiType == AIType.YeuMa)
            {
                Yeu_Ma_Land.SetActive(true);
                avtYeuMa.SetActive(true);
                opponentNameText.text = "Yeu Ma";
                z = Random.Range(91, 119);
            }
            else if (aiType == AIType.LacDieu)
            {
                Lac_Dieu_Land.SetActive(true);
                avtLacDieu.SetActive(true);
                opponentNameText.text = "Lac Dieu";
                z = Random.Range(120, 135);
            }
            deck[i] = CardDatabase.cardList[z];
        }
    }

    // Update is called once per frame
    void Update()
    {
        staticEnemyDeck = deck;

        if (deckSize < 30) cardInDeck1.SetActive(false);
        if (deckSize < 20) cardInDeck2.SetActive(false);
        if (deckSize < 10) cardInDeck3.SetActive(false);
        if (deckSize < 5) cardInDeck4.SetActive(false);
        if (deckSize < 3) cardInDeck5.SetActive(false);
        if (deckSize < 2) cardInDeck6.SetActive(false);
        if (deckSize < 1) cardInDeck7.SetActive(false);

        if (AICardToHand.DrawX > 0)
        {
            StartCoroutine(Draw(AICardToHand.DrawX));
            AICardToHand.DrawX = 0;
        }

        int handSize = Hand.transform.childCount;


        if (TurnSystem.startTurn == false && draw == false && TurnSystem.isYourTurn == false)
        {
            if (handSize < 5)
            {
                StartCoroutine(Draw(1));
                draw = true;
            }
        }
        currentMana = TurnSystem.currentEnemyMana;

        if (0 == 0)
        {
            int j = 0;
            howManyCards = 0;
            foreach (Transform child in Hand.transform)
            {
                howManyCards++;
            }
            foreach (Transform child in Hand.transform)
            {
                cardsInHand[j] = child.GetComponent<AICardToHand>().thisCard[0];
                j++;
            }
            for (int i = 0; i < 40; i++)
            {
                if (i >= howManyCards)
                {
                    cardsInHand[i] = CardDatabase.cardList[0];
                }
            }
            j = 0;
        }

        if (TurnSystem.isYourTurn == false)
        {
            for (int i = 0; i < 40; i++)
            {
                if (cardsInHand[i].id != 0)
                {
                    if (currentMana >= cardsInHand[i].cost)
                    {
                        AiCanSummon[i] = true;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < 40; i++)
            {
                AiCanSummon[i] = false;
            }
        }

        if (TurnSystem.isYourTurn == false)
        {
            drawPhase = true;
        }

        if (drawPhase == true && summonPhase == false && attackPhase == false && Hand.transform.childCount >= 5)
        {
            StartCoroutine(WaitForSummonPhase());
        }

        if (TurnSystem.isYourTurn == true)
        {
            drawPhase = false;
            summonPhase = false;
            attackPhase = false;
            endPhase = false;
        }

        if (summonPhase == true)
        {
            bool hasSummoned = false;
            do
            {
                hasSummoned = false;
                summonID = 0;
                summonThisID = 0;
                int index = 0;

                // Lọc ra các lá có thể triệu hồi
                for (int i = 0; i < 40; i++)
                {
                    if (AiCanSummon[i] == true && cardsInHand[i].cost <= currentMana)
                    {
                        cardsID[index] = cardsInHand[i].id;
                        index++;
                    }
                }

                // Chọn lá có ID lớn nhất
                for (int i = 0; i < 40; i++)
                {
                    if (cardsID[i] != 0)
                    {
                        if (cardsID[i] > summonID)
                        {
                            summonID = cardsID[i];
                        }
                    }
                }

                summonThisID = summonID;

                foreach (Transform child in Hand.transform)
                {
                    if (child.GetComponent<AICardToHand>().id == summonThisID && CardDatabase.cardList[summonThisID].cost <= currentMana)
                    {
                        foreach (GameObject zone in Zones)
                        {
                            if (zone.transform.childCount == 0)
                            {
                                child.transform.SetParent(zone.transform);
                                child.transform.localPosition = Vector3.zero;
                                child.transform.localRotation = Quaternion.identity;
                                child.transform.localScale = Vector3.zero;
                                child.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

                                if (TurnSystem.currentEnemyMana >= CardDatabase.cardList[summonThisID].cost)
                                {
                                    TurnSystem.currentEnemyMana -= CardDatabase.cardList[summonThisID].cost;
                                    SoundManager.PlaySound(SoundType.Summon);
                                    hasSummoned = true;
                                }
                                else
                                {
                                    hasSummoned = false;
                                }
                                break;
                            }
                        }
                    }
                }
            } while (hasSummoned); // Lặp lại nếu triệu hồi thành công
            summonPhase = false;
            attackPhase = true;
        }

        if (0 == 0)
        {
            int k = 0;
            howManyCards_2 = 0;

            foreach (GameObject zone in Zones)
            {
                foreach (Transform child in zone.transform)
                {
                    howManyCards_2++;
                    canAttack[k] = child.GetComponent<AICardToHand>().canAttack;
                    k++;
                }
            }
            for (int i = 0; i < 40; i++)
            {
                if (i >= howManyCards_2)
                {
                    canAttack[i] = false;
                }
            }
            k = 0;
        }

        if (0 == 0)
        {
            int l = 0;
            howManyCards_3 = 0;

            foreach (GameObject zone in Zones)
            {
                foreach (Transform child in zone.transform)
                {
                    howManyCards_3++;
                    cardInZone[l] = child.GetComponent<AICardToHand>().thisCard[0];
                    l++;
                }
            }
            for (int i = 0; i < 40; i++)
            {
                if (i >= howManyCards_3)
                {
                    cardInZone[i] = CardDatabase.cardList[0];
                }
            }
            l = 0;
        }

        if (attackPhase == true && endPhase == false)
        {
            bool playerHasCards = false;
            List<Transform> playerFieldCards = new List<Transform>();

            // Kiểm tra xem player có lá bài nào trên sân không
            foreach (GameObject zone in playerZones)
            {
                if (zone.transform.childCount > 0)
                {
                    playerHasCards = true;
                    foreach (Transform child in zone.transform)
                    {
                        playerFieldCards.Add(child);
                    }
                }
            }

            // Tấn công bài trên sân
            if (playerHasCards)
            {
                int attackerIndex = 0;

                foreach (Transform enemyCard in Zones.SelectMany<GameObject, Transform>(z => z.transform.Cast<Transform>()))
                {
                    if (attackerIndex >= cardInZone.Count || !canAttack[attackerIndex]) continue;
                    if (playerFieldCards.Count == 0) break;

                    Card attacker = cardInZone[attackerIndex];
                    bool attacked = false;

                    // Tìm một mục tiêu mà có thể gây sát thương >= thủ
                    foreach (Transform target in playerFieldCards)
                    {
                        var targetCardScript = target.GetComponent<ThisCard>();
                        Card targetCard = targetCardScript.thisCard[0];

                        if (attacker.attack >= targetCard.defense)
                        {
                            targetCardScript.hurted = attacker.attack;

                            CameraShake.instance.Shake();
                            SoundManager.PlaySound(SoundType.Attack);

                            canAttack[attackerIndex] = false;
                            playerFieldCards.Remove(target); // Xóa mục tiêu đã bị tấn công
                            attacked = true;
                            break;
                        }
                    }

                    if (attacked)
                    {
                        attackerIndex++;
                    }
                }

            }
            else
            {
                // Nếu không còn bài trên sân player thì đánh thẳng máu
                for (int i = 0; i < 40; i++)
                {
                    if (canAttack[i])
                    {
                        PlayerHp.staticHp -= cardInZone[i].attack;
                        CameraShake.instance.Shake();
                        SoundManager.PlaySound(SoundType.Attack);

                        canAttack[i] = false;
                    }
                }
            }

            endPhase = true;
        }

        if (endPhase == true)
        {
            AiEndPhase = true;
        }

    }

    public void Shuffle()
    {
        for (int i = 0; i < deckSize; i++)
        {
            container[0] = deck[i];
            int randomIndex = Random.Range(i, deckSize);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = container[0];
        }
        Instantiate(CardBack, transform.position, transform.rotation);
        StartCoroutine(ShuffleNow());
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
            // Instantiate(CardToHand, transform.position, transform.rotation, Hand.transform);
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

    IEnumerator Draw(int z)
    {
        for (int i = 0; i < z; i++)
        {
            yield return new WaitForSeconds(1f);
            SoundManager.PlaySound(SoundType.Draw);
            Instantiate(aiCardToHand, transform.position, transform.rotation, Hand.transform);
        }
    }

    IEnumerator WaitFiveSeconds()
    {
        yield return new WaitForSeconds(5f);
    }

    IEnumerator WaitForSummonPhase()
    {
        yield return new WaitForSeconds(5f);
        summonPhase = true;
    }

}
