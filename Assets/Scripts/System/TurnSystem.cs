using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class TurnSystem : MonoBehaviour
{
    public static bool isYourTurn;
    public int yourTurn;
    public int yourOpponentTurn;

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI enemyManaText;
    public TextMeshProUGUI turnText;


    public int random;
    public int seconds;
    public bool turnEnd;
    public bool timerStart;

    public static int maxMana;
    public static int currentMana;
    public static int maxEnemyMana;
    public static int currentEnemyMana;

    public static bool startTurn;
    public static bool landConfirmed = false;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 30;
        timerStart = true;

        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (isYourTurn == true)
        {
            turnText.text = "Your Turn";
            yourTurn = 1;
            yourOpponentTurn = 0;
        } else turnText.text = "Opponent's Turn";

        manaText.text = currentMana + "/" + maxMana;

        if (isYourTurn == true && seconds > 0 && timerStart == true && landConfirmed)
        {
            StartCoroutine(Timer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == true)
        {
            EndYourTurn();
            timerStart = true;
            seconds = 30;
        }

        timerText.text = seconds + "";

        if (isYourTurn == false && seconds > 0 && timerStart == true && landConfirmed)
        {
            StartCoroutine(EnemyTimer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == false)
        {
            EndOpponentTurn();
            timerStart = true;
            seconds = 30;
        }

        enemyManaText.text = currentEnemyMana + "/" + maxEnemyMana;

        if (AI.AiEndPhase == true)
        {
            EndOpponentTurn();
            AI.AiEndPhase = false;
        }
    }
    
    public void EndYourTurn()
    {
        if (!landConfirmed) return;

        isYourTurn = false;
        yourOpponentTurn++;
        maxEnemyMana++;
        currentEnemyMana = maxEnemyMana;

        AI.draw = false;

        timerStart = true;
        seconds = 30;

    }

    public void EndOpponentTurn()
    {
        if (!landConfirmed) return;

        isYourTurn = true;
        yourTurn++;
        maxMana++;
        currentMana = maxMana;

        startTurn = true;
        timerStart = true;
        seconds = 30;
        
        AI.draw = false;
    }

    public void StartGame()
    {
        maxMana = 2;
        currentMana = 2;
        maxEnemyMana = 2;
        currentMana = 2;

        if (!landConfirmed) return;
        random = Random.Range(0, 2);

        if (random == 0)
        {
            isYourTurn = true;
            yourTurn = 1;
            yourOpponentTurn = 0;
            startTurn = false;
        }

        if (random == 1)
        {
            isYourTurn = false;
            yourTurn = 0;
            yourOpponentTurn = 1;
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        if (isYourTurn == true && seconds > 0)
        {
            // yield return new WaitForSeconds(1f);
            seconds--;
            StartCoroutine(Timer());
        }
    }

    IEnumerator EnemyTimer()
    {
        yield return new WaitForSeconds(1f);

        if (isYourTurn == false && seconds > 0)
        {
            // yield return new WaitForSeconds(1f);
            seconds--;
            StartCoroutine(EnemyTimer());
        }
    }
}
