using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] private TMP_InputField EmailLoginInput;
    [SerializeField] private TMP_InputField PasswordLoginInput;
    [SerializeField] private GameObject LoginPage;

    [Header("Register")]
    [SerializeField] private TMP_InputField UsernameRegisterInput;
    [SerializeField] private TMP_InputField EmailRegisterInput;
    [SerializeField] private TMP_InputField PasswordRegisterInput;
    [SerializeField] private GameObject RegisterPage;

    [Header("Recovery")]
    [SerializeField] private TMP_InputField EmailRecoveryInput;
    [SerializeField] private GameObject RecoveryPage;

    public void RegisterUser()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Username = UsernameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterInput.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnregisterSuccess, OnError);
    }

    public void Login()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = EmailLoginInput.text,
            Password = PasswordLoginInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        MessageText.text = "Logged in";

        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), accountResult =>
        {
            string username = accountResult.AccountInfo.Username;
            PlayerPrefs.SetString("Username", username);

            // Load dữ liệu khác từ PlayFab
            PlayfabGoldManager.Instance.LoadGoldFromPlayfab(() =>
            {
                LevelUnlockManager.Instance.LoadLevelUnlocks(() =>
                {
                    // Load bộ sưu tập bài
                    Collection collection = FindObjectOfType<Collection>();
                    if (collection != null)
                    {
                        collection.LoadCardsFromPlayfab(() =>
                        {
                            // Sau khi load Collection, load deck
                            DeckCreator deckCreator = FindObjectOfType<DeckCreator>();
                            if (deckCreator != null)
                            {
                                deckCreator.LoadDeckFromPlayfab(() =>
                                {
                                    // Sau khi đã load hết, chuyển scene
                                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                                });
                            }
                            else
                            {
                                Debug.LogWarning("Không tìm thấy DeckCreator trong scene.");
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                            }
                        });
                    }
                    else
                    {
                        Debug.LogWarning("Không tìm thấy script Collection trong scene.");
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                });
            });
        }, OnError);
    }

    private void SaveDefaultDeckToPlayfab(int[] deck, System.Action onDone = null)
    {
        string json = Newtonsoft.Json.JsonConvert.SerializeObject(deck);
        var request = new PlayFab.ClientModels.UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "Deck", json } }
        };

        PlayFab.PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Default deck saved to PlayFab.");
            onDone?.Invoke();
        }, error =>
        {
            Debug.LogError("Save default deck failed: " + error.GenerateErrorReport());
            onDone?.Invoke();
        });
    }

    public void RecoverUser()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = EmailRecoveryInput.text,
            TitleId = "10E280",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFabError result)
    {
        MessageText.text = "No Email Found";
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        OpenLoginPage();
        MessageText.text = "Recovery Mail Sent";
    }

    private void OnError(PlayFabError Error)
    {
        MessageText.text = Error.ErrorMessage;
        Debug.Log(Error.GenerateErrorReport());
    }

    private void OnregisterSuccess(RegisterPlayFabUserResult Result)
    {
        MessageText.text = "New Account is created";
        PlayerPrefs.SetString("Username", UsernameRegisterInput.text);
        PlayerPrefs.Save();

        // TẠO DECK MẶC ĐỊNH và LƯU LUÔN LÊN PLAYFAB
        int numberOfCardsInDatabase = 136; // hoặc lấy số lượng đúng theo game của bạn
        int[] defaultDeck = new int[numberOfCardsInDatabase];
        for (int i = 0; i < 40; i++) defaultDeck[i] = 1;

        SaveDefaultDeckToPlayfab(defaultDeck, () => {
            // Đảm bảo deck đã được lưu
            DeckCreator.lastDeckLoaded = (int[])defaultDeck.Clone();
            OpenLoginPage();
        });
    }
    


    public void OpenLoginPage()
    {
        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(false);
    }

    public void OpenRegisterPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        RecoveryPage.SetActive(false);
    }

    public void OpenRecoveryPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(true);
    }
}
