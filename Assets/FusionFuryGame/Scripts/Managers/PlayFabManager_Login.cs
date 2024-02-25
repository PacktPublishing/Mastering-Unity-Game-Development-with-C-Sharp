using PlayFab.ClientModels;
using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayFabManager 
{
    private LoginManager loginManager;
    private string savedEmailKey = "SavedEmail";
    private void Start()
    {
        loginManager = new LoginManager();

        // Check if email is saved
        if (PlayerPrefs.HasKey(savedEmailKey))
        {
            string savedEmail = PlayerPrefs.GetString(savedEmailKey);
            // Auto-login with saved email
            EmailLoginButtonClicked(savedEmail, "SavedPassword");
        }
    }

    // Example method for triggering email login
    public void EmailLoginButtonClicked(string email, string password)
    {
        loginManager.SetLoginMethod(new EmailLogin(email, password));
        loginManager.Login(OnLoginSuccess, OnLoginFailure);
    }

    // Example method for triggering device ID login
    public void DeviceIDLoginButtonClicked(string deviceID)
    {
        loginManager.SetLoginMethod(new DeviceLogin(deviceID));
        loginManager.Login(OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
        // You can handle success here, such as loading player data

        // Save email for future auto-login
        PlayerPrefs.SetString(savedEmailKey, result.InfoResultPayload.AccountInfo.OpenIdInfo[0].ConnectionId);
        // Load player data
        LoadPlayerData(result.PlayFabId);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failed: " + error.ErrorMessage);
    }

    private void LoadPlayerData(string playFabId)
    {
        var request = new GetUserDataRequest
        {
            PlayFabId = playFabId
        };

        PlayFabClientAPI.GetUserData(request, OnDataSuccess, OnDataFailure);
    }

    private void OnDataSuccess(GetUserDataResult result)
    {
        // Process player data here
        Debug.Log("Player data loaded successfully");
    }

    private void OnDataFailure(PlayFabError error)
    {
        Debug.LogError("Failed to load player data: " + error.ErrorMessage);
    }
}
