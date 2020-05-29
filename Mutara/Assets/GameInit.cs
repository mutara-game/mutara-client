using System;
using System.Threading.Tasks;
using Mutara.Network;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mutara 
{
    public class GameInit : MonoBehaviour
    {
        async void Start()
        {
            var playerSettings = PlayerSettingsAccess.Instance.GetSettings();
            if (!playerSettings.UserAccountCreated)
            {
                playerSettings.UserId = Guid.NewGuid();
                playerSettings.Password = Guid.NewGuid().ToString();
                Debug.Log($"creating a user account for new UserId {playerSettings.UserId}");
                playerSettings.UserSub = await CreateUserAccount(playerSettings);
                PlayerSettingsAccess.Instance.SaveSettings(playerSettings);
            }

            string s = await CallSignIn(playerSettings);
            // TODO save this token somewhere useful! (not player settings..)
            // TODO remove Amazon.Cognito* if it turns out to be lame and not needed.
            // EditorUtility.DisplayDialog("Clicked", s, "Great");
            SceneManager.LoadScene("SampleScene");
        }
        
        
        private async Task<string> CreateUserAccount(PlayerSettings settings)
        {
            var createAccountResponse =
                await NetworkManager.Instance.Auth.CreateAccount(settings.UserId, settings.Password);
            return createAccountResponse.UserSub;
        }

        private async Task<string> CallSignIn(PlayerSettings settings)
        {
            var signInResponse = await NetworkManager.Instance.Auth.SignIn(settings.UserId, settings.Password);
            // TODO error handling..
            return signInResponse.IdToken;
        }
    }
}