using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;

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

    // private void OnLoginSuccess(LoginResult result)
    // {
    //     MessageText.text = "Logged in";

    //     PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), accountResult =>
    //     {
    //         string username = accountResult.AccountInfo.Username;
    //         PlayerPrefs.SetString("Username", username);
    //         PlayerPrefs.Save();

    //         PlayfabGoldManager.Instance.LoadGoldFromPlayfab(() =>
    //         {
    //             LevelUnlockManager.Instance.LoadLevelUnlocks(() =>
    //             {
    //                 // Load bộ sưu tập bài sau khi đăng nhập thành công
    //                 Collection collection = FindObjectOfType<Collection>();
    //                 if (collection != null)
    //                 {
    //                     collection.LoadCardsFromPlayfab(() =>
    //                     {
    //                         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //                     });
    //                 }
    //                 else
    //                 {
    //                     Debug.LogWarning("Không tìm thấy script Collection trong scene.");
    //                     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //                 }
    //             });
    //         });
    //     }, OnError);
    // }
    private void OnLoginSuccess(LoginResult result)
    {
        MessageText.text = "Logged in";

        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), accountResult =>
        {
            string username = accountResult.AccountInfo.Username;
            PlayerPrefs.SetString("Username", username);

            // Kiểm tra xem đã có deck lưu trong PlayerPrefs chưa
            bool hasDeck = false;
            for (int i = 1; i <= 117; i++)
            {
                if (PlayerPrefs.HasKey("deck" + i) && PlayerPrefs.GetInt("deck" + i) > 0)
                {
                    hasDeck = true;
                    break;
                }
            }

            // Nếu chưa có deck thì tạo bộ bài mặc định gồm 40 lá có ID từ 1 đến 40
            if (!hasDeck)
            {
                for (int i = 1; i <= 40; i++)
                {
                    PlayerPrefs.SetInt("deck" + i, 1); // mỗi lá 1 bản
                }
                for (int i = 41; i <= 117; i++)
                {
                    PlayerPrefs.SetInt("deck" + i, 0);
                }
            }

            PlayerPrefs.Save();

            // Load các dữ liệu khác từ PlayFab sau khi đăng nhập
            PlayfabGoldManager.Instance.LoadGoldFromPlayfab(() =>
            {
                LevelUnlockManager.Instance.LoadLevelUnlocks(() =>
                {
                    // Load bộ sưu tập bài sau khi đăng nhập thành công
                    Collection collection = FindObjectOfType<Collection>();
                    if (collection != null)
                    {
                        collection.LoadCardsFromPlayfab(() =>
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        OpenLoginPage();
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
