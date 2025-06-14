using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(play);
    }

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
}
