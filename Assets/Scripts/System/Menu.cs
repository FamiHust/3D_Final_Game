using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string deck;
    public string collection;
    public string Settings;
    public string menu;
    public string shop;

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
        SceneManager.LoadScene(menu);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
