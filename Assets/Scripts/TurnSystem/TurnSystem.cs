using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TurnSystem : MonoBehaviour
{
    public static bool isYourTurn;

    public int turnCount;
    public int playerTurnCount = 0;
    public int enemyTurnCount = 0;
    [SerializeField] private float lerpSpeed = 5f;

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyManaText;
    public Text turnText;
    public GameObject TurnText;

    [SerializeField] private Slider manaSlider;
    [SerializeField] private Slider enemyManaSlider;

    private float displayedMana;
    private float displayedEnemyMana;

    public int seconds = 30;
    public bool timerStart;

    public static int maxMana;
    public static int currentMana;
    public static int maxEnemyMana;
    public static int currentEnemyMana;

    public static bool startTurn;
    public static bool landConfirmed = false;

    private Coroutine timerCoroutine;
    private Coroutine turnTextCoroutine;

    public GameObject EndYourTurnBtn;

    private bool isProcessingTurn = false;
    private bool gameStarted = false;

    private Coroutine blinkCoroutine;
    private Color defaultTimerColor;
    
    // Sử dụng System.Random để có random seed tốt hơn
    private System.Random systemRandom;

    void Start()
    {
        manaSlider.maxValue = maxMana;
        enemyManaSlider.maxValue = maxEnemyMana;

        displayedMana = currentMana;
        displayedEnemyMana = currentEnemyMana;

        defaultTimerColor = timerText.color;
        
        // Khởi tạo System.Random với seed dựa trên thời gian thực
        systemRandom = new System.Random((int)(DateTime.Now.Ticks % int.MaxValue));
    }

    void Update()
    {
        if (!gameStarted) return;

        EndYourTurnBtn.SetActive(isYourTurn);

        displayedMana = Mathf.Lerp(displayedMana, currentMana, Time.deltaTime * lerpSpeed);
        displayedEnemyMana = Mathf.Lerp(displayedEnemyMana, currentEnemyMana, Time.deltaTime * lerpSpeed);

        manaSlider.maxValue = maxMana;
        enemyManaSlider.maxValue = maxEnemyMana;

        manaSlider.value = displayedMana;
        enemyManaSlider.value = displayedEnemyMana;

        manaText.text = currentMana + "/" + maxMana;
        enemyManaText.text = currentEnemyMana + "/" + maxEnemyMana;

        timerText.text = seconds.ToString();

        if (seconds <= 10 && timerStart)
        {
            if (blinkCoroutine == null)
                blinkCoroutine = StartCoroutine(BlinkTimerText());
        }
        else
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
                timerText.color = defaultTimerColor;
            }
        }

        if (landConfirmed && !isProcessingTurn)
        {
            if (isYourTurn && seconds > 0 && timerStart && timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(Timer());
            }

            if (!isYourTurn && seconds > 0 && timerStart && timerCoroutine == null)
            {
                timerCoroutine = StartCoroutine(EnemyTimer());
            }

            if (seconds == 0 && isYourTurn)
            {
                EndYourTurn();
            }

            if (seconds == 0 && !isYourTurn)
            {
                EndOpponentTurn();
            }

            if (AI.AiEndPhase)
            {
                AI.AiEndPhase = false;
                EndOpponentTurn();
            }
        }
    }

    public void EndYourTurn()
    {
        if (!landConfirmed || isProcessingTurn) return;
        isProcessingTurn = true;
        SoundManager.PlaySound(SoundType.NextTurn);

        ForceCancelAllDrags();

        StopTimer();

        isYourTurn = false;
        turnCount++;
        playerTurnCount++;

        if (maxEnemyMana < 8)
            maxEnemyMana++;

        currentEnemyMana = maxEnemyMana;

        AI.draw = false;

        ShowTurnText("Lượt đối thủ");
        StartCoroutine(DelayStartNextTurn(false));
    }

    public void EndOpponentTurn()
    {
        if (!landConfirmed || isProcessingTurn) return;
        isProcessingTurn = true;
        SoundManager.PlaySound(SoundType.NextTurn);

        StopTimer();

        isYourTurn = true;
        turnCount++;
        enemyTurnCount++;

        if (maxMana < 8)
            maxMana++;

        currentMana = maxMana;

        startTurn = true;

        ShowTurnText("Lượt của bạn");
        StartCoroutine(DelayStartNextTurn(true));
    }

    private IEnumerator DelayStartNextTurn(bool nextIsPlayer)
    {
        yield return new WaitForSeconds(0.5f);

        seconds = 30;
        timerStart = true;
        isProcessingTurn = false;
    }

    public void StartGame()
    {
        maxMana = 2;
        currentMana = 2;
        maxEnemyMana = 2;
        currentEnemyMana = 2;

        if (!landConfirmed) return;

        StartCoroutine(DelayedStartGame());
    }

    private IEnumerator DelayedStartGame()
    {
        yield return new WaitForSeconds(5f); 

        // Sử dụng System.Random để có kết quả ngẫu nhiên tốt hơn
        int random = systemRandom.Next(0, 2);
        if (random == 0)
        {
            isYourTurn = true;
            turnCount = 1;
            playerTurnCount = 1;
            enemyTurnCount = 0;
            ShowTurnText("Lượt của bạn");
        }
        else
        {
            isYourTurn = false;
            turnCount = 0;
            playerTurnCount = 0;
            enemyTurnCount = 1;
            ShowTurnText("Lượt đối thủ");
        }

        seconds = 30;
        timerStart = true;
        gameStarted = true;
    }

    private void StopTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }

        timerStart = false;

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        timerText.color = defaultTimerColor;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        if (isYourTurn && seconds > 0)
        {
            seconds--;
            timerCoroutine = StartCoroutine(Timer());
        }
        else
        {
            timerCoroutine = null;
        }
    }

    IEnumerator EnemyTimer()
    {
        yield return new WaitForSeconds(1f);

        if (!isYourTurn && seconds > 0)
        {
            seconds--;
            timerCoroutine = StartCoroutine(EnemyTimer());
        }
        else
        {
            timerCoroutine = null;
        }
    }

    private void ShowTurnText(string message)
    {
        if (turnTextCoroutine != null)
            StopCoroutine(turnTextCoroutine);

        turnTextCoroutine = StartCoroutine(ShowTurnTextRoutine(message));
    }

    private IEnumerator ShowTurnTextRoutine(string message)
    {
        turnText.text = message;
        TurnText.SetActive(true);
        yield return new WaitForSeconds(4f);
        TurnText.SetActive(false);
    }

    private void ForceCancelAllDrags()
    {
        Draggable[] draggables = FindObjectsOfType<Draggable>();
        foreach (Draggable drag in draggables)
        {
            drag.ForceEndDrag();
        }
    }

    private IEnumerator BlinkTimerText()
    {
        while (seconds <= 10 && timerStart)
        {
            timerText.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timerText.color = defaultTimerColor;
            yield return new WaitForSeconds(0.5f);
        }
        timerText.color = defaultTimerColor;
    }
}
