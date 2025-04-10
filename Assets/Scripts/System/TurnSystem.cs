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
    public TextMeshProUGUI turnText;

    public static int maxMana;
    public static int currentMana;
    public TextMeshProUGUI manaText;

    public static bool startTurn;

    public int random;
    public bool turnEnd;
    public TextMeshProUGUI timerText;
    public int seconds;
    public bool timerStart;

    public static int maxEnemyMana;
    public static int currentEnemyMana;
    public TextMeshProUGUI enemyManaText;

    // Start is called before the first frame update
    void Start()
    {
        seconds = 20;
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

        if (isYourTurn == true && seconds > 0 && timerStart == true)
        {
            StartCoroutine(Timer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == true)
        {
            EndYourTurn();
            timerStart = true;
            seconds = 20;
        }

        timerText.text = seconds + "";

        if (isYourTurn == false && seconds > 0 && timerStart == true)
        {
            StartCoroutine(EnemyTimer());
            timerStart = false;
        }

        if (seconds == 0 && isYourTurn == false)
        {
            EndOpponentTurn();
            timerStart = true;
            seconds = 20;
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
        isYourTurn = false;
        yourOpponentTurn++;
        maxEnemyMana++;
        currentEnemyMana = maxEnemyMana;

        AI.draw = false;

        timerStart = true;
        seconds = 20;

    }

    public void EndOpponentTurn()
    {
        isYourTurn = true;
        yourTurn ++;
        maxMana ++;
        currentMana = maxMana;

        startTurn = true;
        timerStart = true;
        seconds = 20;
    }

    public void StartGame()
    {
        random = Random.Range(0, 2);
        if (random == 0)
        {
            isYourTurn = true;
            yourTurn = 1;
            yourOpponentTurn = 0;

            maxMana = 2;
            currentMana = 2;
            maxEnemyMana = 2;
            currentMana = 2;

            startTurn = false;
        }

        if (random == 1)
        {
            isYourTurn = false;
            yourTurn = 0;
            yourOpponentTurn = 1;

            maxMana = 2;
            currentMana = 2;
            maxEnemyMana = 2;
            currentEnemyMana = 2;
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
