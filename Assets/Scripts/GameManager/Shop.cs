using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI diamondText;

    [HideInInspector] public int gold;
    [HideInInspector] public int diamond;

    [SerializeField] private bool playDuel;
    [SerializeField] private GameObject GoldPanel;
    [SerializeField] private GameObject MoneyPanel;
    [SerializeField] private GameObject GemPanel;

    void Start()
    {
        PlayfabGoldManager.Instance.OnGoldChanged += UpdateCurrencyUI;
        PlayfabGoldManager.Instance.OnDiamondChanged += UpdateCurrencyUI;
        UpdateCurrencyUI();
    }

    void Update()
    {
        if (!playDuel)
        {
            UpdateCurrencyUI();
        }
        else
        {
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.SetInt("diamond", diamond);
        }
    }

    void UpdateCurrencyUI()
    {
        gold = PlayfabGoldManager.Instance.Gold;
        diamond = PlayfabGoldManager.Instance.Diamond;

        goldText.text = gold.ToString();
        diamondText.text = diamond.ToString();
    }

    public void BuyPackX1()
    {
        if (PlayfabGoldManager.Instance.Gold >= 10000)
        {
            PlayfabGoldManager.Instance.ChangeGold(-10000);
            SceneManager.LoadScene("OpenPack");
        }
    }

    public void BuySpecialPackWithDiamond()
    {
        if (PlayfabGoldManager.Instance.Diamond >= 10)
        {
            PlayfabGoldManager.Instance.ChangeDiamond(-10);
            SceneManager.LoadScene("OpenPack");
        }
        else
        {
            Debug.Log("Not enough diamonds!");
        }
    }

    public void OpenGoldPanel()
    {
        GoldPanel.SetActive(true);
        MoneyPanel.SetActive(false);
        GemPanel.SetActive(false);
    }

    public void OpenMoneyPanel()
    {
        GoldPanel.SetActive(false);
        MoneyPanel.SetActive(true);
        GemPanel.SetActive(false);
    }

    public void OpenGemPanel()
    {
        GoldPanel.SetActive(false);
        MoneyPanel.SetActive(false);
        GemPanel.SetActive(true);
    }
}
