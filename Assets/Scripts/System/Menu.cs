using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string play;
    public string map;
    public string deck;
    public string collection;
    public string Settings;
    public string menu;
    public string shop;

    public GameObject ConcedeDefeat;

    public void LoadPlay()
    {
        SceneManager.LoadScene(play);
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(map);
    }

    public void LoadDeck()
    {
        SceneManager.LoadScene(deck);
    }

    public void LoadCollection()
    {
        SceneManager.LoadScene(collection);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(shop);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(menu);
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

    public void ExitGame()
    {
        Application.Quit();
    }
}
