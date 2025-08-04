using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    [SerializeField] private string play;
    [SerializeField] private string map;
    [SerializeField] private string deck;
    [SerializeField] private string tutorial;
    [SerializeField] private string Settings;
    [SerializeField] private string menu;
    [SerializeField] private string shop;

    [SerializeField] private GameObject ConcedeDefeat;

    [Header("UI Transition")]
    [SerializeField] private RectTransform leftDoor;
    [SerializeField] private RectTransform rightDoor;
    [SerializeField] private float doorDuration = 1.5f;

    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        StartCoroutine(TransitionAndLoad(level));
    }

    IEnumerator TransitionAndLoad(int level)
    {
        // Wait few seconds before starting door animation
        yield return new WaitForSeconds(1f);

        // Animate doors closing (assuming anchored left/right offscreen at start)
        leftDoor.DOAnchorPosX(0, doorDuration);
        rightDoor.DOAnchorPosX(0, doorDuration);

        // Wait for doors to close
        yield return new WaitForSeconds(doorDuration);

        // Now load scene
        SceneManager.LoadScene(level);
    }


    public void LoadPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(play);
    }

    // public void LoadLevel(int level)
    // {
    //     StartCoroutine(Delay(3f));
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(level);
    // }

    public void LoadMap()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(map);
    }

    public void LoadDeck()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(deck);
    }

    public void LoadTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(tutorial);
    }

    public void LoadShop()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(shop);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menu);
    }

    public void ReturnToLoadingMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LoadingMenu");
    }

    public void ConcedeDefeated()
    {
        Time.timeScale = 0;
        ConcedeDefeat.SetActive(true);
    }

    public void ExitConcedeDefeated()
    {
        Time.timeScale = 1;
        ConcedeDefeat.SetActive(false);
    }

    public void Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator Delay(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
    }
}
