using System;
using System.Threading.Tasks;
using Mutara.Network;
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

            NetworkManager.Instance.IdToken =  await CallSignIn(playerSettings);
            Debug.Log("Sign In was successful, idToken received");
            
            // go to .... somewhere.
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